package mono.com.microsoft.appcenter.analytics.channel;


public class AnalyticsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.analytics.channel.AnalyticsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBeforeSending:(Lcom/microsoft/appcenter/ingestion/models/Log;)V:GetOnBeforeSending_Lcom_microsoft_appcenter_ingestion_models_Log_Handler:Com.Microsoft.Appcenter.Analytics.Channel.IAnalyticsListenerInvoker, Microsoft.AppCenter.Analytics.Android.Bindings\n" +
			"n_onSendingFailed:(Lcom/microsoft/appcenter/ingestion/models/Log;Ljava/lang/Exception;)V:GetOnSendingFailed_Lcom_microsoft_appcenter_ingestion_models_Log_Ljava_lang_Exception_Handler:Com.Microsoft.Appcenter.Analytics.Channel.IAnalyticsListenerInvoker, Microsoft.AppCenter.Analytics.Android.Bindings\n" +
			"n_onSendingSucceeded:(Lcom/microsoft/appcenter/ingestion/models/Log;)V:GetOnSendingSucceeded_Lcom_microsoft_appcenter_ingestion_models_Log_Handler:Com.Microsoft.Appcenter.Analytics.Channel.IAnalyticsListenerInvoker, Microsoft.AppCenter.Analytics.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Analytics.Channel.IAnalyticsListenerImplementor, Microsoft.AppCenter.Analytics.Android.Bindings", AnalyticsListenerImplementor.class, __md_methods);
	}


	public AnalyticsListenerImplementor ()
	{
		super ();
		if (getClass () == AnalyticsListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Analytics.Channel.IAnalyticsListenerImplementor, Microsoft.AppCenter.Analytics.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onBeforeSending (com.microsoft.appcenter.ingestion.models.Log p0)
	{
		n_onBeforeSending (p0);
	}

	private native void n_onBeforeSending (com.microsoft.appcenter.ingestion.models.Log p0);


	public void onSendingFailed (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.Exception p1)
	{
		n_onSendingFailed (p0, p1);
	}

	private native void n_onSendingFailed (com.microsoft.appcenter.ingestion.models.Log p0, java.lang.Exception p1);


	public void onSendingSucceeded (com.microsoft.appcenter.ingestion.models.Log p0)
	{
		n_onSendingSucceeded (p0);
	}

	private native void n_onSendingSucceeded (com.microsoft.appcenter.ingestion.models.Log p0);

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
