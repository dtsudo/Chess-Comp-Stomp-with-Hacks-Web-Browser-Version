
namespace ChessCompStompWithHacks
{
	using Bridge;
	using ChessCompStompWithHacksLibrary;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	
	public class BridgeKeyboard : IKeyboard
	{
		public BridgeKeyboard(bool disableArrowKeyScrolling)
		{
			Script.Eval(@"
				window.BridgeKeyboardJavascript = ((function () {
					'use strict';
					
					var keysBeingPressed = [];
					var keyPressesThatNeedToBeProcessed = [];
					
					var disableArrowKeyScrolling = " + (disableArrowKeyScrolling ? "true" : "false") + @";
					
					var mapKeyToCanonicalKey = function (key) {
						if (key === 'A')
							return 'a';
						if (key === 'B')
							return 'b';
						if (key === 'C')
							return 'c';
						if (key === 'D')
							return 'd';
						if (key === 'E')
							return 'e';
						if (key === 'F')
							return 'f';
						if (key === 'G')
							return 'g';
						if (key === 'H')
							return 'h';
						if (key === 'I')
							return 'i';
						if (key === 'J')
							return 'j';
						if (key === 'K')
							return 'k';
						if (key === 'L')
							return 'l';
						if (key === 'M')
							return 'm';
						if (key === 'N')
							return 'n';
						if (key === 'O')
							return 'o';
						if (key === 'P')
							return 'p';
						if (key === 'Q')
							return 'q';
						if (key === 'R')
							return 'r';
						if (key === 'S')
							return 's';
						if (key === 'T')
							return 't';
						if (key === 'U')
							return 'u';
						if (key === 'V')
							return 'v';
						if (key === 'W')
							return 'w';
						if (key === 'X')
							return 'x';
						if (key === 'Y')
							return 'y';
						if (key === 'Z')
							return 'z';
						if (key === '!')
							return '1';
						if (key === '@')
							return '2';
						if (key === '#')
							return '3';
						if (key === '$')
							return '4';
						if (key === '%')
							return '5';
						if (key === '^')
							return '6';
						if (key === '&')
							return '7';
						if (key === '*')
							return '8';
						if (key === '(')
							return '9';
						if (key === ')')
							return '0';
						
						return key;
					};
					
					var keyDownHandler = function (e) {
						
						if (disableArrowKeyScrolling) {
							if (e.key === 'ArrowRight' || e.key === 'ArrowLeft' || e.key === 'ArrowUp' || e.key === 'ArrowDown' || e.key === ' ')
								e.preventDefault();
						}
						
						var key = mapKeyToCanonicalKey(e.key);
						
						var shouldAdd = true;
						for (let i = 0; i < keysBeingPressed.length; i++) {
							if (keysBeingPressed[i] === key) {
								shouldAdd = false;
								break;
							}
						}
						
						if (shouldAdd)
							keysBeingPressed.push(key);
						
						for (let i = 0; i < keyPressesThatNeedToBeProcessed.length; i++) {
							if (keyPressesThatNeedToBeProcessed[i] === key)
								return;
						}
						
						keyPressesThatNeedToBeProcessed.push(key);
					};
					
					var keyUpHandler = function (e) {
						var key = mapKeyToCanonicalKey(e.key);
						
						var newArray = [];
						
						for (var i = 0; i < keysBeingPressed.length; i++) {
							if (keysBeingPressed[i] !== key)
								newArray.push(keysBeingPressed[i]);
						}
						
						keysBeingPressed = newArray;
					};
					
					var processedInputs = function () {
						keyPressesThatNeedToBeProcessed = [];
					};

					document.addEventListener('keydown', function (e) { keyDownHandler(e); }, false);
					document.addEventListener('keyup', function (e) { keyUpHandler(e); }, false);
					
					var isKeyPressed = function (k) {
						for (let i = 0; i < keysBeingPressed.length; i++) {
							if (keysBeingPressed[i] === k)
								return true;
						}

						for (let i = 0; i < keyPressesThatNeedToBeProcessed.length; i++) {
							if (keyPressesThatNeedToBeProcessed[i] === k)
								return true;
						}
						
						return false;
					};
					
					return {
						isKeyPressed: isKeyPressed,
						processedInputs: processedInputs
					};
				})());
			");
		}

		public void ProcessedInputs()
		{
			Script.Write("window.BridgeKeyboardJavascript.processedInputs()");
		}
		
		public bool IsPressed(Key key)
		{
			string correspondingKeyCode;
			
			switch (key)
			{
				case Key.A:
					correspondingKeyCode = "a";
					break;
				case Key.B:
					correspondingKeyCode = "b";
					break;
				case Key.C:
					correspondingKeyCode = "c";
					break;
				case Key.D:
					correspondingKeyCode = "d";
					break;
				case Key.E:
					correspondingKeyCode = "e";
					break;
				case Key.F:
					correspondingKeyCode = "f";
					break;
				case Key.G:
					correspondingKeyCode = "g";
					break;
				case Key.H:
					correspondingKeyCode = "h";
					break;
				case Key.I:
					correspondingKeyCode = "i";
					break;
				case Key.J:
					correspondingKeyCode = "j";
					break;
				case Key.K:
					correspondingKeyCode = "k";
					break;
				case Key.L:
					correspondingKeyCode = "l";
					break;
				case Key.M:
					correspondingKeyCode = "m";
					break;
				case Key.N:
					correspondingKeyCode = "n";
					break;
				case Key.O:
					correspondingKeyCode = "o";
					break;
				case Key.P:
					correspondingKeyCode = "p";
					break;
				case Key.Q:
					correspondingKeyCode = "q";
					break;
				case Key.R:
					correspondingKeyCode = "r";
					break;
				case Key.S:
					correspondingKeyCode = "s";
					break;
				case Key.T:
					correspondingKeyCode = "t";
					break;
				case Key.U:
					correspondingKeyCode = "u";
					break;
				case Key.V:
					correspondingKeyCode = "v";
					break;
				case Key.W:
					correspondingKeyCode = "w";
					break;
				case Key.X:
					correspondingKeyCode = "x";
					break;
				case Key.Y:
					correspondingKeyCode = "y";
					break;
				case Key.Z:
					correspondingKeyCode = "z";
					break;
				case Key.Zero:
					correspondingKeyCode = "0";
					break;
				case Key.One:
					correspondingKeyCode = "1";
					break;
				case Key.Two:
					correspondingKeyCode = "2";
					break;
				case Key.Three:
					correspondingKeyCode = "3";
					break;
				case Key.Four:
					correspondingKeyCode = "4";
					break;
				case Key.Five:
					correspondingKeyCode = "5";
					break;
				case Key.Six:
					correspondingKeyCode = "6";
					break;
				case Key.Seven:
					correspondingKeyCode = "7";
					break;
				case Key.Eight:
					correspondingKeyCode = "8";
					break;
				case Key.Nine:
					correspondingKeyCode = "9";
					break;
				case Key.UpArrow:
					correspondingKeyCode = "ArrowUp";
					break;
				case Key.DownArrow:
					correspondingKeyCode = "ArrowDown";
					break;
				case Key.LeftArrow:
					correspondingKeyCode = "ArrowLeft";
					break;
				case Key.RightArrow:
					correspondingKeyCode = "ArrowRight";
					break;
				case Key.Delete:
					correspondingKeyCode = "Delete";
					break;
				case Key.Backspace:
					correspondingKeyCode = "Backspace";
					break;
				case Key.Enter:
					correspondingKeyCode = "Enter";
					break;
				case Key.Shift:
					correspondingKeyCode = "Shift";
					break;
				case Key.Space:
					correspondingKeyCode = " ";
					break;
				case Key.Esc:
					correspondingKeyCode = "Escape";
					break;
				default:
					throw new Exception();
			}
						
			bool result = Script.Call<bool>("window.BridgeKeyboardJavascript.isKeyPressed", correspondingKeyCode);
			
			if (result)
				return true;
			
			return false;
			
		}
	}
}
