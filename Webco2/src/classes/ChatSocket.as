package classes
{
	import flash.events.DataEvent;
	import flash.events.Event;
	import flash.net.Socket;
	
	public class ChatSocket extends Event
	{
		private var socket:Socket;
		public static const MESSAGE_RECEIVED:String = "messageReceived";
		private var message:String;
		
		public function ChatSocket(type:String,message:String=null)
		{
			super(type);
			socket =  = new Socket();
			socket.connect("192.168.137.2",4804);
			socket.addEventListener(DataEvent.DATA,onData);
		}
		public function get message():String{
			return message;
		}
		override public function clone():Event{
			return new ChatSocket(type,message);
		}
		public function sendMessage(msgSend:String):void{
			socket.send();
		}
		private function onData(e:DataEvent):void{
			dispatchEvent(new ChatSocket(ChatSocket.MESSAGE_RECEIVED,e.data);
		}

	}
}