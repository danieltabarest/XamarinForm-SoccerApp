package md55e01038d7640b0d4dbbb8416c21f5b0f;


public class DialogService_OnDismissListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.DialogInterface.OnDismissListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDismiss:(Landroid/content/DialogInterface;)V:GetOnDismiss_Landroid_content_DialogInterface_Handler:Android.Content.IDialogInterfaceOnDismissListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("GalaSoft.MvvmLight.Views.DialogService+OnDismissListener, GalaSoft.MvvmLight.Platform, Version=5.3.0.19037, Culture=neutral, PublicKeyToken=null", DialogService_OnDismissListener.class, __md_methods);
	}


	public DialogService_OnDismissListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DialogService_OnDismissListener.class)
			mono.android.TypeManager.Activate ("GalaSoft.MvvmLight.Views.DialogService+OnDismissListener, GalaSoft.MvvmLight.Platform, Version=5.3.0.19037, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onDismiss (android.content.DialogInterface p0)
	{
		n_onDismiss (p0);
	}

	private native void n_onDismiss (android.content.DialogInterface p0);

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
