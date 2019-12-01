package mono.com.microsoft.appcenter.channel;


public class Channel_GroupListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.channel.Channel.GroupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBeforeSending:(Lcom/microsoft/appcenter/ingestion/models/Log;)V:GetOnBeforeSending_Lcom_microsoft_appcenter_ingestion_models_Log_Handler:Com.Microsoft.Appcenter.Channel.IChannelGroupListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onFailure:(Lcom/microsoft/appcenter/ingestion/models/Log;Ljava/lang/Exception;)V:GetOnFailure_Lcom_microsoft_appcenter_ingestion_models_Log_Ljava_lang_Exception_Handler:Com.Microsoft.Appcenter.Channel.IChannelGroupListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onSuccess:(Lcom/microsoft/appcenter/ingestion/models/Log;)V:GetOnSuccess_Lcom_microsoft_appcenter_ingestion_models_Log_Handler:Com.Microsoft.Appcenter.Channel.IChannelGroupListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Channel.IChannelGroupListenerImplementor, Microsoft.AppCenter.Android.Bindings", Channel_GroupListenerImplementor.class, __md_methods);
	}


	public Channel_GroupListenerImplementor ()
	{
		super ();
		if (getClass () == Channel_GroupListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Channel.IChannelGroupListenerImplementor, Microsoft.AppCenter.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onBeforeSending (com.microsoft.appcenter.ingestion.models.Log p0)
	{
		n_onBeforeSending (p0);
	}

	private native void n_onBeforeSending (com.microsoft.appcenter.ingestion.models.Log p0);


	public void onFailure (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.Exception p1)
	{
		n_onFailure (p0, p1);
	}

	private native void n_onFailure (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.Exception p1);


	public void onSuccess (com.microsoft.appcenter.ingestion.models.Log p0)
	{
		n_onSuccess (p0);
	}

	private native void n_onSuccess (com.microsoft.appcenter.ingestion.models.Log p0);

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
