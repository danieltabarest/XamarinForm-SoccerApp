package mono.com.microsoft.appcenter.utils.context;


public class AuthTokenContext_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.utils.context.AuthTokenContext.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNewAuthToken:(Ljava/lang/String;)V:GetOnNewAuthToken_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Utils.Context.AuthTokenContext/IListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onNewUser:(Ljava/lang/String;)V:GetOnNewUser_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Utils.Context.AuthTokenContext/IListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"n_onTokenRequiresRefresh:(Ljava/lang/String;)V:GetOnTokenRequiresRefresh_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Utils.Context.AuthTokenContext/IListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Utils.Context.AuthTokenContext+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", AuthTokenContext_ListenerImplementor.class, __md_methods);
	}


	public AuthTokenContext_ListenerImplementor ()
	{
		super ();
		if (getClass () == AuthTokenContext_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Utils.Context.AuthTokenContext+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onNewAuthToken (java.lang.String p0)
	{
		n_onNewAuthToken (p0);
	}

	private native void n_onNewAuthToken (java.lang.String p0);


	public void onNewUser (java.lang.String p0)
	{
		n_onNewUser (p0);
	}

	private native void n_onNewUser (java.lang.String p0);


	public void onTokenRequiresRefresh (java.lang.String p0)
	{
		n_onTokenRequiresRefresh (p0);
	}

	private native void n_onTokenRequiresRefresh (java.lang.String p0);

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
