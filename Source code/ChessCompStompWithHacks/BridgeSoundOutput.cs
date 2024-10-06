
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class BridgeSoundOutput : ISoundOutput<GameSound>
	{	
		private int desiredSoundVolume;
		private int currentSoundVolume;
		private int elapsedMicrosPerFrame;
		
		public BridgeSoundOutput(int elapsedMicrosPerFrame)
		{
			this.desiredSoundVolume = GlobalState.DEFAULT_VOLUME;
			this.currentSoundVolume = this.desiredSoundVolume;
			this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;

			if (BridgeUtil.IsMobileSafari())
			{
				Script.Write(@"
					window.BridgeSoundOutputJavascript = ((function () {
						'use strict';

						let soundDictionary = {};
										
						let audioContext = new AudioContext();
					
						let numberOfAudioObjectsLoaded = 0;
						let numberOfAudioObjects = null;

						let loadSounds = function (oggSoundNames, flacSoundNames) {
							let oggSoundNamesArray = oggSoundNames.split(',');
							let flacSoundNamesArray = flacSoundNames.split(',');
						
							numberOfAudioObjects = oggSoundNamesArray.length;

							for (let i = 0; i < oggSoundNamesArray.length; i++) {
								let soundName = oggSoundNamesArray[i];
								let flacSoundName = flacSoundNamesArray[i];
							
								if (soundDictionary[soundName])
									continue;
							
								soundDictionary[soundName] = { audioBuffer: null };

								numberOfAudioObjectsLoaded++;
							
								let soundPath = 'Data/Sound/' + flacSoundName + '?doNotCache=' + Date.now().toString();

								fetch(soundPath)
									.then(response => response.blob())
									.then(blob => blob.arrayBuffer())
									.then(arrayBuffer => audioContext.decodeAudioData(arrayBuffer))
									.then(audioBuffer => { soundDictionary[soundName].audioBuffer = audioBuffer; });
							}
						
							return true;
						};

						let getNumElementsLoaded = function () {
							return numberOfAudioObjectsLoaded;
						};

						let getNumTotalElementsToLoad = function () {
							return numberOfAudioObjects;
						};
					
						let playSound = function (soundName, volume) {
							if (volume > 1.0)
								volume = 1.0;
							if (volume < 0.0)
								volume = 0.0;
						
							if (volume <= 0.0)
								return;

							let audioBuffer = soundDictionary[soundName].audioBuffer;

							if (audioBuffer === null)
								return;

							let source = audioContext.createBufferSource();
							source.buffer = audioBuffer;
							let gainNode = new GainNode(audioContext, { gain: volume });
							source.connect(gainNode);
							gainNode.connect(audioContext.destination);
							source.start();
						};
					
						return {
							loadSounds: loadSounds,
							getNumElementsLoaded: getNumElementsLoaded,
							getNumTotalElementsToLoad: getNumTotalElementsToLoad,
							playSound: playSound
						};
					})());
				");
			}
			else
			{
				Script.Write(@"
					window.BridgeSoundOutputJavascript = ((function () {
						'use strict';

						let soundDictionary = {};
					
						let numberOfAudioObjectsLoaded = 0;
						let numberOfAudioObjects = null;
					
						let loadSounds = function (oggSoundNames, flacSoundNames) {
							let oggSoundNamesArray = oggSoundNames.split(',');
							let flacSoundNamesArray = flacSoundNames.split(',');
						
							numberOfAudioObjects = oggSoundNamesArray.length * 4;
						
							for (let i = 0; i < oggSoundNamesArray.length; i++) {
								let soundName = oggSoundNamesArray[i];
								let flacSoundName = flacSoundNamesArray[i];
							
								if (soundDictionary[soundName])
									continue;
							
								soundDictionary[soundName] = [];
							
								let soundPath = 'Data/Sound/' + soundName + '?doNotCache=' + Date.now().toString();
								for (let j = 0; j < 4; j++) {
									let hasAudioLoadingSucceeded = false;
									let audio = new Audio();
									audio.addEventListener('canplaythrough', function () {
										if (!hasAudioLoadingSucceeded) {
											numberOfAudioObjectsLoaded++;
										}

										hasAudioLoadingSucceeded = true;
									});

									audio.src = soundPath;
									audio.load();

									let checkForError;
									checkForError = function () {
										if (hasAudioLoadingSucceeded)
											return;
										if (audio.error !== null) {
											audio.src = 'Data/Sound/' + flacSoundName + '?doNotCache=' + Date.now().toString();
											audio.load();
											return;
										}

										setTimeout(checkForError, 50 /* arbitrary */);
									};
									setTimeout(checkForError, 0);

									soundDictionary[soundName].push(audio);
								}
							}
						
							return numberOfAudioObjects === numberOfAudioObjectsLoaded;
						};

						let getNumElementsLoaded = function () {
							return numberOfAudioObjectsLoaded;
						};

						let getNumTotalElementsToLoad = function () {
							return numberOfAudioObjects;
						};
					
						var playSound = function (soundName, volume) {
							var sound = soundDictionary[soundName];
						
							if (volume > 1.0)
								volume = 1.0;
							if (volume < 0.0)
								volume = 0.0;
						
							var audio = sound[0];
						
							for (var i = 0; i < sound.length; i++) {
								if (i === sound.length - 1)
									sound[i] = audio;
								else
									sound[i] = sound[i+1];
							}
						
							audio.volume = volume;
							audio.play();
						};
					
						return {
							loadSounds: loadSounds,
							getNumElementsLoaded: getNumElementsLoaded,
							getNumTotalElementsToLoad: getNumTotalElementsToLoad,
							playSound: playSound
						};
					})());
				");
			}
		}
	
		public bool LoadSounds()
		{			
			string oggSoundNames = "";
			string flacSoundNames = "";
			bool isFirst = true;
			foreach (GameSound gameSound in Enum.GetValues(typeof(GameSound)))
			{
				if (isFirst)
				{
					isFirst = false;
				}
				else
				{
					oggSoundNames = oggSoundNames + ",";
					flacSoundNames = flacSoundNames + ",";
				}
				
				string oggSoundFilename = gameSound.GetSoundFilename().OggFilename;
				oggSoundNames = oggSoundNames + oggSoundFilename;

				string flacSoundFilename = gameSound.GetSoundFilename().FlacFilename;
				flacSoundNames = flacSoundNames + flacSoundFilename;
			}
			
			if (oggSoundNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.BridgeSoundOutputJavascript.loadSounds('" + oggSoundNames + "', '" + flacSoundNames + "')");
			
			return result;
		}

		public int GetNumElementsLoaded()
		{
			return Script.Call<int>("window.BridgeSoundOutputJavascript.getNumElementsLoaded");
		}

		public int? GetNumTotalElementsToLoad()
		{
			return Script.Call<int?>("window.BridgeSoundOutputJavascript.getNumTotalElementsToLoad");
		}

		/// <summary>
		/// Volume ranges from 0 to 100 (both inclusive)
		/// </summary>
		public void SetSoundVolume(int volume)
		{
			if (volume < 0)
				throw new Exception();

			if (volume > 100)
				throw new Exception();

			this.desiredSoundVolume = volume;
		}
		
		public void SetSoundVolumeImmediately(int volume)
		{
			if (volume < 0)
				throw new Exception();

			if (volume > 100)
				throw new Exception();

			this.desiredSoundVolume = volume;
			this.currentSoundVolume = volume;
		}

		public int GetSoundVolume()
		{
			return this.desiredSoundVolume;
		}

		public void ProcessFrame()
		{
			this.currentSoundVolume = VolumeUtil.GetVolumeSmoothed(
				elapsedMicrosPerFrame: this.elapsedMicrosPerFrame,
				currentVolume: this.currentSoundVolume,
				desiredVolume: this.desiredSoundVolume);
		}
		
		public void PlaySound(GameSound sound)
		{
			double finalVolume = (sound.GetSoundVolume() / 100.0) * (this.currentSoundVolume / 100.0);
			if (finalVolume > 1.0)
				finalVolume = 1.0;
			if (finalVolume < 0.0)
				finalVolume = 0.0;
			
			if (finalVolume > 0.0)
			{
				string soundFilename = sound.GetSoundFilename().OggFilename;
				Script.Call("window.BridgeSoundOutputJavascript.playSound", soundFilename, finalVolume);
			}
		}
		
		public void DisposeSounds()
		{
		}
	}
}
