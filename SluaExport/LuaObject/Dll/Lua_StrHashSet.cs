﻿using System;

using SLua;
using System.Collections.Generic;
public class Lua_StrHashSet : LuaObject {
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			StrHashSet o;
			if(argc==1){
				o=new StrHashSet();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==2){
				System.Collections.Generic.IEnumerable<System.String> a1;
				checkType(l,2,out a1);
				o=new StrHashSet(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Add(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.Add(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Clear(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			self.Clear();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Contains(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.Contains(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Remove(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.Remove(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int RemoveWhere(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Predicate<System.String> a1;
			LuaDelegation.checkDelegate(l,2,out a1);
			var ret=self.RemoveWhere(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int TrimExcess(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			self.TrimExcess();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IntersectWith(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			self.IntersectWith(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int ExceptWith(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			self.ExceptWith(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int Overlaps(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.Overlaps(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SetEquals(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.SetEquals(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int SymmetricExceptWith(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			self.SymmetricExceptWith(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int UnionWith(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			self.UnionWith(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsSubsetOf(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.IsSubsetOf(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsProperSubsetOf(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.IsProperSubsetOf(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsSupersetOf(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.IsSupersetOf(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int IsProperSupersetOf(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Collections.Generic.IEnumerable<System.String> a1;
			checkType(l,2,out a1);
			var ret=self.IsProperSupersetOf(a1);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int GetObjectData(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Runtime.Serialization.SerializationInfo a1;
			checkType(l,2,out a1);
			System.Runtime.Serialization.StreamingContext a2;
			checkValueType(l,3,out a2);
			self.GetObjectData(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int OnDeserialization(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			System.Object a1;
			checkType(l,2,out a1);
			self.OnDeserialization(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_Count(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static public int get_Comparer(IntPtr l) {
		try {
			StrHashSet self=(StrHashSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Comparer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"StrHashSet");
		addMember(l,Add);
		addMember(l,Clear);
		addMember(l,Contains);
		addMember(l,Remove);
		addMember(l,RemoveWhere);
		addMember(l,TrimExcess);
		addMember(l,IntersectWith);
		addMember(l,ExceptWith);
		addMember(l,Overlaps);
		addMember(l,SetEquals);
		addMember(l,SymmetricExceptWith);
		addMember(l,UnionWith);
		addMember(l,IsSubsetOf);
		addMember(l,IsProperSubsetOf);
		addMember(l,IsSupersetOf);
		addMember(l,IsProperSupersetOf);
		addMember(l,GetObjectData);
		addMember(l,OnDeserialization);
		addMember(l,"Count",get_Count,null,true);
		addMember(l,"Comparer",get_Comparer,null,true);
		createTypeMetatable(l,constructor, typeof(StrHashSet),typeof(System.Collections.Generic.HashSet<System.String>));
	}
}
