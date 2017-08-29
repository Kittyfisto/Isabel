using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Speech.Recognition;
using System.Threading;
using log4net;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     Uses the windows speech recognition engine.
	/// </summary>
	public sealed class WindowsSpeechRecognitionEngine
		: AbstractSpeechRecognitionEngine
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly SpeechRecognitionEngine _engine;
		private readonly ConcurrentQueue<RecognitionResult> _results;

		private readonly object _templateSyncRoot;
		private readonly Dictionary<ITemplate, InstalledTemplate> _installedTemplates;
		private readonly Dictionary<Grammar, InstalledVoiceCommandTemplate> _installedTemplateByGrammar;
		private readonly ICommandFactory _commandFactory;

		public WindowsSpeechRecognitionEngine(ICommandExecutionEngine commandExecutionEngine,
			ICommandFactory commandFactory,
			IConfiguration configuration)
			: base(commandExecutionEngine)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			_results = new ConcurrentQueue<RecognitionResult>();

			_templateSyncRoot = new object();
			_installedTemplates = new Dictionary<ITemplate, InstalledTemplate>();
			_installedTemplateByGrammar = new Dictionary<Grammar, InstalledVoiceCommandTemplate>();

			_commandFactory = commandFactory;

			_engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
			_engine.SpeechRecognized += EngineOnSpeechRecognized;

			_engine.SetInputToDefaultAudioDevice();
		}

		private void EngineOnSpeechRecognized(object sender, SpeechRecognizedEventArgs args)
		{
			_results.Enqueue(args.Result);
		}

		private bool TryFindInstalledTemplate(Grammar grammar, out InstalledVoiceCommandTemplate installedTemplate)
		{
			lock (_installedTemplateByGrammar)
			{
				return _installedTemplateByGrammar.TryGetValue(grammar, out installedTemplate);
			}
		}

		protected override ICommand RecognizeNextCommand(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				RecognitionResult result;
				if (_results.TryDequeue(out result))
				{
					return TryCreateCommandFrom(result);
				}

				Thread.Sleep(TimeSpan.FromMilliseconds(100));
			}

			return null;
		}

		private ICommand TryCreateCommandFrom(RecognitionResult result)
		{
			var grammar = result.Grammar;
			Log.DebugFormat("Speech recognized: {0}, Confidence {1}%", result.Text, (int)(result.Confidence * 100));
			InstalledVoiceCommandTemplate installedTemplate;
			if (!TryFindInstalledTemplate(grammar, out installedTemplate))
			{
				Log.InfoFormat("Recognized speech {0} uses unknown grammar '{1}', ignoring it...", result.Text, grammar);
				return null;
			}

			ICommand command = installedTemplate.Command;
			return command;
		}

		protected override void DisposeAdditional()
		{
			_engine.Dispose();
		}

		public override void AddTemplate(ITemplate template)
		{
			lock (_templateSyncRoot)
			{
				bool wasEmpty = _installedTemplates.Count == 0;

				var installedTemplate = new InstalledTemplate(template, _commandFactory);
				_installedTemplates.Add(template, installedTemplate);
				foreach (var foo in installedTemplate.Templates)
				{
					var grammar = foo.Grammar;
					_installedTemplateByGrammar.Add(grammar, foo);
					_engine.LoadGrammar(grammar);
				}

				//_engine.RecognizeAsyncCancel();
				//_engine.RecognizeAsyncStop();
				_engine.RecognizeAsync(RecognizeMode.Multiple);
			}
		}

		public override void RemoveTemplate(ITemplate template)
		{
			lock (_templateSyncRoot)
			{
				InstalledTemplate installedTemplate;
				if (_installedTemplates.TryGetValue(template, out installedTemplate))
				{
					foreach (var foo in installedTemplate.Templates)
					{
						var grammar = foo.Grammar;
						_engine.UnloadGrammar(grammar);
						_installedTemplateByGrammar.Remove(grammar);
					}
					_installedTemplates.Remove(template);
				}
			}
		}

		internal sealed class InstalledVoiceCommandTemplate
		{
			private readonly VoiceCommandTemplate _template;
			private readonly Grammar _grammar;
			private readonly ICommand _command;

			public InstalledVoiceCommandTemplate(VoiceCommandTemplate template, ICommandFactory commandFactory)
			{
				if (template == null)
					throw new ArgumentNullException(nameof(template));
				if (commandFactory == null)
					throw new ArgumentNullException(nameof(commandFactory));

				_template = template;
				var builder = new GrammarBuilder();

				var phrase = template.Phrase;
				var commandTemplate = template.Command;

				builder.Append(new Choices(phrase));
				_command = commandFactory.TryCreate(commandTemplate);
				if (_command == null)
				{
					Log.WarnFormat("Unable to create a command for {0}", commandTemplate);
				}

				_grammar = new Grammar(builder);
				
			}

			public Grammar Grammar => _grammar;

			public VoiceCommandTemplate Template => _template;

			public ICommand Command => _command;
		}

		internal sealed class InstalledTemplate
		{
			private readonly IReadOnlyDictionary<Grammar, InstalledVoiceCommandTemplate> _templates;

			public InstalledTemplate(ITemplate template, ICommandFactory commandFactory)
			{
				var templates = new Dictionary<Grammar, InstalledVoiceCommandTemplate>(template.VoiceCommands.Count);
				foreach (var voiceCommandTemplate in template.VoiceCommands)
				{
					var installedTemplate = new InstalledVoiceCommandTemplate(voiceCommandTemplate, commandFactory);
					templates.Add(installedTemplate.Grammar, installedTemplate);
				}
				_templates = templates;
			}

			public IEnumerable<InstalledVoiceCommandTemplate> Templates => _templates.Values;
		}
	}
}