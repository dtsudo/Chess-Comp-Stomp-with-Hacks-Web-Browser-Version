
namespace ChessCompStompWithHacks
{
	using Bridge;

	public class BridgeUtil
	{
		public static bool IsMobileSafari()
		{
			bool isMobileSafari = Script.Eval<bool>(@"
				((function () {
					let isDesktop = window.matchMedia('(pointer:fine)').matches;
					
					if (isDesktop)
						return false;
				
					let userAgent = window.navigator.userAgent.toLowerCase();
					
					if (userAgent.includes('chrome'))
						return false;
						
					if (userAgent.includes('chromium'))
						return false;
					
					if (!userAgent.includes('safari'))
						return false;
				
					return true;
				})())
			");

			if (isMobileSafari)
				return true;
			else
				return false;
		}
	}
}
