package{
	import flash.media.Sound;
	import flash.net.URLRequest;
	public class Suara{
		var sound:Sound = new Sound();;
		public function Suara(url){
			sound.load(new URLRequest(url));
			sound.play();
		}
		public function bunyi(url){
			
		}
	}
}