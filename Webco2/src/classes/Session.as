package classes
{
	import flash.utils.Dictionary;


	[Bindable]
	
	public class Session
	{
		private static var instance:Session = new Session();
		private var dic:Dictionary=new Dictionary(); 
		
		
		public function Session()
		{
			if (instance != null) { throw new Error('Cannot create a new instance.  Must use Session.getInstance().') }
		}
		
		public static function getInstance():Session
		{
			return instance;
		}
		

		public function setAttribute(key:*,value:*):void
		{
			dic[key]=value;
		}
		
		public function getAttribute(key:*):*
		{
			return dic[key];
		}
		
		
	}
}