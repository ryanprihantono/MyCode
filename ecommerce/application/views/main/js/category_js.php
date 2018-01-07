<script type="text/javascript">
	//categories accordion
	$(function() {
		$( "#categories" ).accordion({
			collapsible: true
		});
	});
	
	
	//product description tooltip pop up
	var minMargin = 15;
	var ready = false; 
	var default_width = 200;
	
	$(".product-content a").mouseover(function(){
		popup(this.name,200);
	});

	jQuery(document).ready(function(){
		$('body').append('<div id="pup" style="position:abolute; display:none; z-index:200;"></div>');
		css_width = $('#pup').width();
		if (css_width != 0) default_width = css_width;

		$(document).mousemove(function(e){ 
			var x,y;
		  
			x = $(document).scrollLeft() + e.clientX;
			y = $(document).scrollTop() + e.clientY;
	
			x += 10;
		  
			var x_y = nudge(x,y);

			$('#pup').css('top', x_y[1] + 'px');
			$('#pup').css('left', x_y[0] + 'px');
		});
		ready = true;
	});
	
	function popup(msg, width){
		if (ready) {

			if (typeof width === "undefined"){
				width = default_width;
			}

			$('#pup').html(msg).width(width).show();

			var t = getTarget(arguments.callee.caller.arguments[0]);
			$(t).unbind('mouseout').bind('mouseout', 
				function(e){
					$('#pup').hide().width(default_width);
				}
			);
		}
	}
	
	function nudge(x,y){
		var win = $(window);

		var xtreme = $(document).scrollLeft() + win.width() - $('#pup').width() - minMargin;
		if(x > xtreme) {
			x -= $('#pup').width() + 2 * minMargin;
		}
		x = max(x, 0);
	
		if((y + $('#pup').height()) > (win.height() +  $(document).scrollTop())) {
			y -= $('#pup').height() + minMargin;
		}
	
		return [ x, y ];
	}
	
	function max(a,b){
		if (a>b) return a;
		else return b;
	}

	function getTarget(e) {
		var targ;
		if (!e) var e = window.event;
		if (e.target) targ = e.target;
		else if (e.srcElement) targ = e.srcElement;
		if (targ.nodeType == 3)
			targ = targ.parentNode;
		return targ;
	}
	
	//contact us
	<?php if($mainbar=='contactus'){ ?>
	
	$('#send').click(function(){
		if($('#subject').val()!="" && $('#message')){
			var e_mail = '';
			if('<?php echo $this->session->userdata('email'); ?>' == ''){
				e_mail = $('#email').val();
			}
			else{
				e_mail = '<?php echo $this->session->userdata('email'); ?>';
			}
			if(e_mail!=''){
				$.ajax({
					type: "POST",
					url: "<?php echo base_url(); ?>main/contactus_message",
					data: { 
						subject: $('#subject').val(),
						email: e_mail,
						message: $('#message').val()
					},
					datatype:"json",
					success: function(response) {
						if(response.status=="success"){
							jSuccess(response.msg,
							{
								autoHide : false, // added in v2.0
								clickOverlay : true, // added in v2.0
								MinWidth : 250,
								TimeShown : 1000,
								ShowTimeEffect : 100,
								HideTimeEffect : 300,
								LongTrip :20,
								HorizontalPosition : 'center',
								VerticalPosition : 'top',
								ShowOverlay : true,
								ColorOverlay : '#000',
								OpacityOverlay : 0.3,
								onClosed : function(){ // added in v2.0
					
								},
								onCompleted : function(){ // added in v2.0
									$('#subject').val('');
									$('#message').val('');
								}
							});
						}
						else{
							jError(response.msg,
							{
								autoHide : false, // added in v2.0
								clickOverlay : true, // added in v2.0
								MinWidth : 250,
								TimeShown : 1000,
								ShowTimeEffect : 100,
								HideTimeEffect : 300,
								LongTrip :20,
								HorizontalPosition : 'center',
								VerticalPosition : 'top',
								ShowOverlay : true,
								ColorOverlay : '#000',
								OpacityOverlay : 0.3,
								onClosed : function(){ // added in v2.0
					
								},
								onCompleted : function(){ // added in v2.0
					
								}
							});
						}
					}
				});
			}
		}
	});
	
	<?php } ?>
</script>