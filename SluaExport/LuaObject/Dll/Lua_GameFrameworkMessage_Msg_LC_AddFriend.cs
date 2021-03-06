﻿using System;

using SLua;
using System.Collections.Generic;
public class Lua_GameFrameworkMessage_Msg_LC_AddFriend : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			GameFrameworkMessage.Msg_LC_AddFriend o;
			o=new GameFrameworkMessage.Msg_LC_AddFriend();
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_m_FriendInfo(IntPtr l) {
		try {
			GameFrameworkMessage.Msg_LC_AddFriend self=(GameFrameworkMessage.Msg_LC_AddFriend)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.m_FriendInfo);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int set_m_FriendInfo(IntPtr l) {
		try {
			GameFrameworkMessage.Msg_LC_AddFriend self=(GameFrameworkMessage.Msg_LC_AddFriend)checkSelf(l);
			GameFrameworkMessage.FriendInfoForMessage v;
			checkType(l,2,out v);
			self.m_FriendInfo=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"GameFrameworkMessage.Msg_LC_AddFriend");
		addMember(l,"m_FriendInfo",get_m_FriendInfo,set_m_FriendInfo,true);
		createTypeMetatable(l,constructor, typeof(GameFrameworkMessage.Msg_LC_AddFriend));
	}
}
