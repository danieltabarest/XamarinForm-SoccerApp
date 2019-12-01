package mono.com.microsoft.appcenter.utils.context;


public class UserIdContext_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.utils.context.UserIdContext.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNewUserId:(Ljava/lang/String;)V:GetOnNewUserId_Ljava_lang_String_Handler:Com.Microsoft.Appcenter.Utils.Context.UserIdContext/IListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Utils.Context.UserIdContext+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", UserIdContext_ListenerImplementor.class, __md_methods);
	}


	public UserIdContext_ListenerImplementor ()
	{
		super ();
		if (getClass () == UserIdContext_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Utils.Context.UserIdContext+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onNewUserId (java.lang.String p0)
	{
		n_onNewUserId (p0);
	}

	private native void n_onNewUserId (java.lang.String p0);

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
