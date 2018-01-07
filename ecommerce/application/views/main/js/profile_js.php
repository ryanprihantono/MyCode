<script type="text/javascript">
	$(function() {
		$( "#tabs" ).tabs();
	});
	var temp;
	function payment(cart_id){
		window.location.replace("<?php echo base_url(); ?>main/payment/"+cart_id);
	}
	function detail(cart_id){
		window.location.replace("<?php echo base_url(); ?>main/detail_transaction/"+cart_id);
	}
	function verify(cart_id){
	}
	$(document).ready(function() {
		var oTable = $('#transactions').dataTable();
	} );
	
	$("#edit_password").click(function(){
		$("#password").before("<input type='password' id='cpassword' class='uniform' placeholder='New Password' /><input type='password' id='conpassword' class='uniform' placeholder='Confirm Password' /><a href='#' id='change_password'><img src='<?php echo base_url(); ?>assets/admin/images/icon/save.png' title='Save Change' witdh='15' height='15' style='margin-left:5px'/></a>").remove();
		$("#change_password").click(function(){
			if($("#cpassword").val()!="" && $("#conpassword").val()!=""){
				if($("#cpassword").val() == $("#conpassword").val()){
					change($("#cpassword").val(),'password');
				}
				else{
					failed("New Password and Confirm Password must be match");
				}
			}
		});
	});
	
	$("#change_email").click(function(){
		if($("#email").val()!=""){
			change($("#email").val(),'email');
		}
	});
	
	$("#change_address").click(function(){
		if($("#address").val()!=""){
			change($("#address").val(),'address');
		}
	});
	$("#change_city").click(function(){
		if($("#city").val()!=""){
			change($("#city").val(),'city');
		}
	});
	$("#change_phone").click(function(){
		if($("#phone").val()!=""){
			change($("#phone").val(),'phone');
		}
	});
	$("#change_birthday").click(function(){
		if($("#birthday").val()!=""){
			change($("#birthday").val(),'birthday');
		}
	});
	$("#change_gender").click(function(){
		alert($("#gender option:selected").val());
		change($("#gender option:selected").val(),'gender');
	});
	function change(val,act){
		$.ajax({
			type: "POST",
			url: "<?php echo base_url(); ?>main/edit_profile",
			data: { 
				action: act,
				value: val
			},
			datatype:"json",
			success: function(response) {
				if(response.status=="success"){
					success(response.msg,act);
				}
				else{
					failed(response.msg);
				}
			}
		});
	}
	function success(msg,act){
		if(act=='password'){
			$("#conpassword").remove();
			$("#change_password").remove();
			$("#cpassword").before("<label class='uniform' id='password' style='font-weight:bold'>Hidden<a href='#' id='edit_password'><img src='<?php echo base_url(); ?>assets/admin/images/icon/edit.png' title='Change Password' witdh='15' height='15' style='margin-left:5px'/></a></label>").remove();
		}
		jSuccess(msg,
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
	function failed(msg){
		jError(msg,
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
	
</script>
