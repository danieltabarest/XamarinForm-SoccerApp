package mono.com.microsoft.appcenter.utils;


public class NetworkStateHelper_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.appcenter.utils.NetworkStateHelper.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNetworkStateUpdated:(Z)V:GetOnNetworkStateUpdated_ZHandler:Com.Microsoft.Appcenter.Utils.NetworkStateHelper/IListenerInvoker, Microsoft.AppCenter.Android.Bindings\n" +
			"";
		mono.android.Runtime.register ("Com.Microsoft.Appcenter.Utils.NetworkStateHelper+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", NetworkStateHelper_ListenerImplementor.class, __md_methods);
	}


	public NetworkStateHelper_ListenerImplementor ()
	{
		super ();
		if (getClass () == NetworkStateHelper_ListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Microsoft.Appcenter.Utils.NetworkStateHelper+IListenerImplementor, Microsoft.AppCenter.Android.Bindings", "", this, new java.lang.Object[] {  });
	}


	public void onNetworkStateUpdated (boolean p0)
	{
		n_onNetworkStateUpdated (p0);
	}

	private native void n_onNetworkStateUpdated (boolean p0);

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
