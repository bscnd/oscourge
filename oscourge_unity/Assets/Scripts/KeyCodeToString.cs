﻿using UnityEngine;
using System.Collections.Generic;

// singleton
public class KeyCodeToString
{
	private static KeyCodeToString instance;

	public static KeyCodeToString Instance(){
		if(KeyCodeToString.instance == null){
			KeyCodeToString.instance = new KeyCodeToString();
		}
		return KeyCodeToString.instance;
	}
	
	private Dictionary<KeyCode, string> stringKeys;

	private KeyCodeToString(){
		this.stringKeys = new Dictionary<KeyCode, string>();

		stringKeys.Add(KeyCode.A, "A");
		stringKeys.Add(KeyCode.Alpha0, "Alpha Keyboard 0");
		stringKeys.Add(KeyCode.Alpha1, "Alpha Keyboard 1");
		stringKeys.Add(KeyCode.Alpha2, "Alpha Keyboard 2");
		stringKeys.Add(KeyCode.Alpha3, "Alpha Keyboard 3");
		stringKeys.Add(KeyCode.Alpha4, "Alpha Keyboard 4");
		stringKeys.Add(KeyCode.Alpha5, "Alpha Keyboard 5");
		stringKeys.Add(KeyCode.Alpha6, "Alpha Keyboard 6");
		stringKeys.Add(KeyCode.Alpha7, "Alpha Keyboard 7");
		stringKeys.Add(KeyCode.Alpha8, "Alpha Keyboard 8");
		stringKeys.Add(KeyCode.Alpha9, "Alpha Keyboard 9");
		stringKeys.Add(KeyCode.AltGr, "Alt Gr");
		stringKeys.Add(KeyCode.Ampersand, "&");
		stringKeys.Add(KeyCode.Asterisk, "*");
		stringKeys.Add(KeyCode.At, "@");
		stringKeys.Add(KeyCode.B, "B");
		stringKeys.Add(KeyCode.BackQuote, "`");
		stringKeys.Add(KeyCode.Backslash, "\\");
		stringKeys.Add(KeyCode.Backspace, "Backspace");
		stringKeys.Add(KeyCode.Break, "Break");
		stringKeys.Add(KeyCode.C, "C");
		stringKeys.Add(KeyCode.CapsLock, "Capslock");
		stringKeys.Add(KeyCode.Caret, "^");
		stringKeys.Add(KeyCode.Clear, "Clear");
		stringKeys.Add(KeyCode.Colon, ":");
		stringKeys.Add(KeyCode.Comma, ",");
		stringKeys.Add(KeyCode.D, "D");
		stringKeys.Add(KeyCode.Delete, "Delete");
		stringKeys.Add(KeyCode.Dollar, "$");
		stringKeys.Add(KeyCode.DoubleQuote, "\"");
		stringKeys.Add(KeyCode.DownArrow, "Down arrow");
		stringKeys.Add(KeyCode.E, "E");
		stringKeys.Add(KeyCode.End, "End");
		stringKeys.Add(KeyCode.Equals, "=");
		stringKeys.Add(KeyCode.Escape, "Escape");
		stringKeys.Add(KeyCode.Exclaim, "!");
		stringKeys.Add(KeyCode.F, "F");
		stringKeys.Add(KeyCode.F1, "F1");
		stringKeys.Add(KeyCode.F10, "F10");
		stringKeys.Add(KeyCode.F11, "F11");
		stringKeys.Add(KeyCode.F12, "F12");
		stringKeys.Add(KeyCode.F13, "F13");
		stringKeys.Add(KeyCode.F14, "F14");
		stringKeys.Add(KeyCode.F15, "F15");
		stringKeys.Add(KeyCode.F2, "F2");
		stringKeys.Add(KeyCode.F3, "F3");
		stringKeys.Add(KeyCode.F4, "F4");
		stringKeys.Add(KeyCode.F5, "F5");
		stringKeys.Add(KeyCode.F6, "F6");
		stringKeys.Add(KeyCode.F7, "F7");
		stringKeys.Add(KeyCode.F8, "F8");
		stringKeys.Add(KeyCode.F9, "F9");
		stringKeys.Add(KeyCode.G, "G");
		stringKeys.Add(KeyCode.Greater, ">");
		stringKeys.Add(KeyCode.H, "H");
		stringKeys.Add(KeyCode.Hash, "#");
		stringKeys.Add(KeyCode.Help, "Help");
		stringKeys.Add(KeyCode.Home, "Home");
		stringKeys.Add(KeyCode.I, "I");
		stringKeys.Add(KeyCode.Insert, "Insert");
		stringKeys.Add(KeyCode.J, "J");
		stringKeys.Add(KeyCode.Joystick1Button0, "Button 0 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button1, "Button 1 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button10, "Button 10 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button11, "Button 11 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button12, "Button 12 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button13, "Button 13 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button14, "Button 14 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button15, "Button 15 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button16, "Button 16 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button17, "Button 17 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button18, "Button 18 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button19, "Button 19 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button2, "Button 2 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button3, "Button 3 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button4, "Button 4 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button5, "Button 5 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button6, "Button 6 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button7, "Button 7 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button8, "Button 8 on first joystick");
		stringKeys.Add(KeyCode.Joystick1Button9, "Button 9 on first joystick");
		stringKeys.Add(KeyCode.Joystick2Button0, "Button 0 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button1, "Button 1 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button10, "Button 10 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button11, "Button 11 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button12, "Button 12 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button13, "Button 13 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button14, "Button 14 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button15, "Button 15 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button16, "Button 16 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button17, "Button 17 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button18, "Button 18 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button19, "Button 19 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button2, "Button 2 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button3, "Button 3 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button4, "Button 4 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button5, "Button 5 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button6, "Button 6 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button7, "Button 7 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button8, "Button 8 on second joystick");
		stringKeys.Add(KeyCode.Joystick2Button9, "Button 9 on second joystick");
		stringKeys.Add(KeyCode.Joystick3Button0, "Button 0 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button1, "Button 1 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button10, "Button 10 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button11, "Button 11 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button12, "Button 12 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button13, "Button 13 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button14, "Button 14 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button15, "Button 15 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button16, "Button 16 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button17, "Button 17 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button18, "Button 18 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button19, "Button 19 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button2, "Button 2 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button3, "Button 3 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button4, "Button 4 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button5, "Button 5 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button6, "Button 6 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button7, "Button 7 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button8, "Button 8 on third joystick");
		stringKeys.Add(KeyCode.Joystick3Button9, "Button 9 on third joystick");
		stringKeys.Add(KeyCode.Joystick4Button0, "Button 0 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button1, "Button 1 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button10, "Button 10 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button11, "Button 11 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button12, "Button 12 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button13, "Button 13 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button14, "Button 14 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button15, "Button 15 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button16, "Button 16 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button17, "Button 17 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button18, "Button 18 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button19, "Button 19 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button2, "Button 2 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button3, "Button 3 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button4, "Button 4 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button5, "Button 5 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button6, "Button 6 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button7, "Button 7 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button8, "Button 8 on forth joystick");
		stringKeys.Add(KeyCode.Joystick4Button9, "Button 9 on forth joystick");
		stringKeys.Add(KeyCode.Joystick5Button0, "Button 0 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button1, "Button 1 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button10, "Button 10 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button11, "Button 11 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button12, "Button 12 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button13, "Button 13 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button14, "Button 14 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button15, "Button 15 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button16, "Button 16 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button17, "Button 17 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button18, "Button 18 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button19, "Button 19 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button2, "Button 2 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button3, "Button 3 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button4, "Button 4 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button5, "Button 5 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button6, "Button 6 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button7, "Button 7 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button8, "Button 8 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick5Button9, "Button 9 on fifth joystick");
		stringKeys.Add(KeyCode.Joystick6Button0, "Button 0 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button1, "Button 1 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button10, "Button 10 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button11, "Button 11 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button12, "Button 12 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button13, "Button 13 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button14, "Button 14 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button15, "Button 15 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button16, "Button 16 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button17, "Button 17 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button18, "Button 18 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button19, "Button 19 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button2, "Button 2 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button3, "Button 3 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button4, "Button 4 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button5, "Button 5 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button6, "Button 6 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button7, "Button 7 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button8, "Button 8 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick6Button9, "Button 9 on sixth joystick");
		stringKeys.Add(KeyCode.Joystick7Button0, "Button 0 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button1, "Button 1 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button10, "Button 10 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button11, "Button 11 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button12, "Button 12 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button13, "Button 13 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button14, "Button 14 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button15, "Button 15 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button16, "Button 16 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button17, "Button 17 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button18, "Button 18 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button19, "Button 19 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button2, "Button 2 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button3, "Button 3 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button4, "Button 4 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button5, "Button 5 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button6, "Button 6 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button7, "Button 7 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button8, "Button 8 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick7Button9, "Button 9 on seventh joystick");
		stringKeys.Add(KeyCode.Joystick8Button0, "Button 0 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button1, "Button 1 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button10, "Button 10 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button11, "Button 11 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button12, "Button 12 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button13, "Button 13 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button14, "Button 14 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button15, "Button 15 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button16, "Button 16 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button17, "Button 17 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button18, "Button 18 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button19, "Button 19 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button2, "Button 2 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button3, "Button 3 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button4, "Button 4 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button5, "Button 5 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button6, "Button 6 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button7, "Button 7 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button8, "Button 8 on eighth joystick");
		stringKeys.Add(KeyCode.Joystick8Button9, "Button 9 on eighth joystick");
		stringKeys.Add(KeyCode.JoystickButton0, "Button 0 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton1, "Button 1 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton10, "Button 10 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton11, "Button 11 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton12, "Button 12 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton13, "Button 13 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton14, "Button 14 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton15, "Button 15 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton16, "Button 16 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton17, "Button 17 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton18, "Button 18 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton19, "Button 19 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton2, "Button 2 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton3, "Button 3 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton4, "Button 4 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton5, "Button 5 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton6, "Button 6 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton7, "Button 7 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton8, "Button 8 on any joystick");
		stringKeys.Add(KeyCode.JoystickButton9, "Button 9 on any joystick");
		stringKeys.Add(KeyCode.K, "K");
		stringKeys.Add(KeyCode.Keypad0, "Keypad 0");
		stringKeys.Add(KeyCode.Keypad1, "Keypad 1");
		stringKeys.Add(KeyCode.Keypad2, "Keypad 2");
		stringKeys.Add(KeyCode.Keypad3, "Keypad 3");
		stringKeys.Add(KeyCode.Keypad4, "Keypad 4");
		stringKeys.Add(KeyCode.Keypad5, "Keypad 5");
		stringKeys.Add(KeyCode.Keypad6, "Keypad 6");
		stringKeys.Add(KeyCode.Keypad7, "Keypad 7");
		stringKeys.Add(KeyCode.Keypad8, "Keypad 8");
		stringKeys.Add(KeyCode.Keypad9, "Keypad 9");
		stringKeys.Add(KeyCode.KeypadDivide, "Keypad /");
		stringKeys.Add(KeyCode.KeypadEnter, "Keypad Enter");
		stringKeys.Add(KeyCode.KeypadEquals, "Keypad =");
		stringKeys.Add(KeyCode.KeypadMinus, "Keypad -");
		stringKeys.Add(KeyCode.KeypadMultiply, "Keypad *");
		stringKeys.Add(KeyCode.KeypadPeriod, "Keypad .");
		stringKeys.Add(KeyCode.KeypadPlus, "Keypad +");
		stringKeys.Add(KeyCode.L, "L");
		stringKeys.Add(KeyCode.LeftAlt, "Left Alt");
		stringKeys.Add(KeyCode.LeftApple, "Left Apple");
		stringKeys.Add(KeyCode.LeftArrow, "Left arrow");
		stringKeys.Add(KeyCode.LeftBracket, "[");
		//stringKeys.Add(KeyCode.LeftCommand, "Left Command");
		stringKeys.Add(KeyCode.LeftControl, "Left Control");
		stringKeys.Add(KeyCode.LeftCurlyBracket, "{");
		stringKeys.Add(KeyCode.LeftParen, "(");
		stringKeys.Add(KeyCode.LeftShift, "Left shift");
		stringKeys.Add(KeyCode.LeftWindows, "Left Windows");
		stringKeys.Add(KeyCode.Less, "<");
		stringKeys.Add(KeyCode.M, "M");
		stringKeys.Add(KeyCode.Menu, "Menu");
		stringKeys.Add(KeyCode.Minus, "-");
		stringKeys.Add(KeyCode.Mouse0, "Primary mouse button");
		stringKeys.Add(KeyCode.Mouse1, "Secondary mouse button");
		stringKeys.Add(KeyCode.Mouse2, "Middle mouse button");
		stringKeys.Add(KeyCode.Mouse3, "Fourth mouse button");
		stringKeys.Add(KeyCode.Mouse4, "Fifth mouse button");
		stringKeys.Add(KeyCode.Mouse5, "Sixth mouse button");
		stringKeys.Add(KeyCode.Mouse6, "Seventh mouse button");
		stringKeys.Add(KeyCode.N, "N");
		stringKeys.Add(KeyCode.None, "Not assigned");
		stringKeys.Add(KeyCode.Numlock, "Numlock");
		stringKeys.Add(KeyCode.O, "O");
		stringKeys.Add(KeyCode.P, "P");
		stringKeys.Add(KeyCode.PageDown, "Page down");
		stringKeys.Add(KeyCode.PageUp, "Page up");
		stringKeys.Add(KeyCode.Pause, "Pause");
		stringKeys.Add(KeyCode.Percent, "%");
		stringKeys.Add(KeyCode.Period, ".");
		stringKeys.Add(KeyCode.Pipe, "|");
		stringKeys.Add(KeyCode.Plus, "+");
		stringKeys.Add(KeyCode.Print, "Print");
		stringKeys.Add(KeyCode.Q, "Q");
		stringKeys.Add(KeyCode.Question, "?");
		stringKeys.Add(KeyCode.Quote, "'");
		stringKeys.Add(KeyCode.R, "R");
		stringKeys.Add(KeyCode.Return, "Return");
		stringKeys.Add(KeyCode.RightAlt, "Right Alt");
		stringKeys.Add(KeyCode.RightApple, "Right Apple");
		stringKeys.Add(KeyCode.RightArrow, "Right arrow");
		stringKeys.Add(KeyCode.RightBracket, "]");
		//stringKeys.Add(KeyCode.RightCommand, "Right Command");
		stringKeys.Add(KeyCode.RightControl, "Right Control");
		stringKeys.Add(KeyCode.RightCurlyBracket, "}");
		stringKeys.Add(KeyCode.RightParen, "(");
		stringKeys.Add(KeyCode.RightShift, "Right shift");
		stringKeys.Add(KeyCode.RightWindows, "Right Windows");
		stringKeys.Add(KeyCode.S, "S");
		stringKeys.Add(KeyCode.ScrollLock, "Scroll lock");
		stringKeys.Add(KeyCode.Semicolon, ";");
		stringKeys.Add(KeyCode.Slash, "/");
		stringKeys.Add(KeyCode.Space, "Space");
		stringKeys.Add(KeyCode.SysReq, "Sys Req");
		stringKeys.Add(KeyCode.T, "T");
		stringKeys.Add(KeyCode.Tab, "Tab");
		stringKeys.Add(KeyCode.Tilde, "~");
		stringKeys.Add(KeyCode.U, "U");
		stringKeys.Add(KeyCode.Underscore, "_");
		stringKeys.Add(KeyCode.UpArrow, "Up arrow");
		stringKeys.Add(KeyCode.V, "V");
		stringKeys.Add(KeyCode.W, "W");
		stringKeys.Add(KeyCode.X, "X");
		stringKeys.Add(KeyCode.Y, "Y");
		stringKeys.Add(KeyCode.Z, "Z");
	}

	public string Convert(KeyCode code){
		if(stringKeys.ContainsKey(code)){
			return stringKeys[code];
		}
		else{
			Debug.LogError("KeyCodeToString::KeyCodeToString -- Cannot convert keycode to string !");
			return null;
		}
	}
}
