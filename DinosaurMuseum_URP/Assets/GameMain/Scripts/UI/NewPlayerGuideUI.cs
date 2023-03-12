namespace VGame
{
	public class NewPlayerGuideUI : UGuiForm
	{
		protected override void OnOpen(object userData)
		{
			base.OnOpen(userData);

			VGameManager.instance.starterAssetsInputs.UIInput(true);
		}

		protected override void OnClose(bool isShutdown, object userData)
		{
			base.OnClose(isShutdown, userData);
			
			VGameManager.instance.starterAssetsInputs.UIInput(false);
		}

		public void OnClick_Mask()
		{
			Close();
		}
	}
}