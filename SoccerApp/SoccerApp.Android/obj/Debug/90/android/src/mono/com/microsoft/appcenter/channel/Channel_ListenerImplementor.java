package mono.com.microsoft.appcenter.channel;


public class Channel_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.channel.Channel.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onClear:(Ljava/lang/String;)V:GetOnClear_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onGloballyEnabled:(Z)V:GetOnGloballyEnabled_ZHandler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onGroupAdded:(Ljava/lang/String;Lcom/microsoft/appcenter/channel/Channel$GroupListener;J)V:GetOnGroupAdded_Ljava_lang_String_Lcom_microsoft_appcenter_channel_Channel_GroupListener_JHandler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onGroupRemoved:(Ljava/lang/String;)V:GetOnGroupRemoved_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onPaused:(Ljava/lang/String;Ljava/lang/String;)V:GetOnPaused_Ljava_lang_String_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onPreparedLog:(Lcom/microsoft/appcenter/ingestion/models/Log;Ljava/lang/String;I)V:GetOnPreparedLog_Lcom_microsoft_appcenter_ingestion_models_Log_Ljava_lang_String_IHandler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onPreparingLog:(Lcom/microsoft/appcenter/ingestion/models/Log;Ljava/lang/String;)V:GetOnPreparingLog_Lcom_microsoft_appcenter_ingestion_models_Log_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onResumed:(Ljava/lang/String;Ljava/lang/String;)V:GetOnResumed_Ljava_lang_String_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_shouldFilter:(Lcom/microsoft/appcenter/ingestion/models/Log;)Z:GetShouldFilter_Lcom_microsoft_appcenter_ingestion_models_Log_Handler:Com.Microsoft.Appcenter.Channel.IChannelListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Channel.IChannelListenerImplementor, Microsoft.AppCenter.Android.Bindings", Channel_ListenerImplementor.class, __md_methods);
	}


	public Channel_ListenerImplementor ()
	{
		super ();
		if (getClass () == Channel_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Channel.IChannelListenerImplementor, Microsoft.AppCenter.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onClear (java.lang.String p0)
	{
		n_onClear (p0);
	}

	private native void n_onClear (java.lang.String p0);


	public void onGloballyEnabled (boolean p0)
	{
		n_onGloballyEnabled (p0);
	}

	private native void n_onGloballyEnabled (boolean p0);


	public void onGroupAdded (java.lang.String p0, com.microsoft.appcenter.channel.Channel.GroupListener p1, long p2)
	{
		n_onGroupAdded (p0, p1, p2);
	}

	private native void n_onGroupAdded (java.lang.String p0, com.microsoft.appcenter.channel.Channel.GroupListener p1, long p2);


	public void onGroupRemoved (java.lang.String p0)
	{
		n_onGroupRemoved (p0);
	}

	private native void n_onGroupRemoved (java.lang.String p0);


	public void onPaused (java.lang.String p0, java.lang.String p1)
	{
		n_onPaused (p0, p1);
	}

	private native void n_onPaused (java.lang.String p0, java.lang.String p1);


	public void onPreparedLog (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.String p1, int p2)
	{
		n_onPreparedLog (p0, p1, p2);
	}

	private native void n_onPreparedLog (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.String p1, int p2);


	public void onPreparingLog (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.String p1)
	{
		n_onPreparingLog (p0, p1);
	}

	private native void n_onPreparingLog (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.String p1);


	public void onResumed (java.lang.String p0, java.lang.String p1)
	{
		n_onResumed (p0, p1);
	}

	private native void n_onResumed (java.lang.String p0, java.lang.String p1);


	public boolean shouldFilter (com.microsoft.appcenter.ingestion.models.Log p0)
	{
		return n_shouldFilter (p0);
	}

	private native boolean n_shouldFilter (com.microsoft.appcenter.ingestion.models.Log p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
