<script type="text/javascript">
	var username=false;
	var email=false;
	var password=false;
	var conpassword=false;
	var address=false;
	var city=false;
	var phone=false;
	$('#username').blur(function(){
		if(this.value==""){
			$('#err_username').html("<font color='red'>Username must be filled</font>");
			username=false;
		}
		else if(this.value.length<6){
			$('#err_username').html("<font color='red'>Username at least consist of 6 characters</font>");
			username=false;
		}
		else{
			$.getJSON('<?php echo base_url().'main/cek_username'; ?>/'+this.value,function(data){
					if(data.availability.availability == "available"){
						$('#err_username').html("<font color='green'>Username is available</font>");
						username=true;
					}
					else{
						$('#err_username').html("<font color='red'>Username is not available</font>");
						username=false;
					}
			});
		}
		enable_submit();
	});
	$('#email').blur(function(){
		if(this.value==""){
			$('#err_email').html("<font color='red'>Email must be filled</font>");
			email=false;
		}
		else{
			email=true;
		}
		enable_submit();
	});

	$('#password').blur(function(){
		if(this.value==""){
			$('#err_password').html("<font color='red'>Password must be filled</font>");
			password=false;
		}
		else{
			password=true;
		}
		enable_submit();
	});
	$('#conpassword').blur(function(){
		if(this.value==""){
			$('#err_conpassword').html("<font color='red'>Confirm Password must be filled</font>");
			conpassword=false;
		}
		else if(this.value != $('#password').val()){
			$('#err_conpassword').html("<font color='red'>Password and Confirm are not matched</font>");
			conpassword=false;
		}
		else{
			conpassword=true;
		}
		enable_submit();
	});
	$('#address').blur(function(){
		if(this.value==""){
			$('#err_address').html("<font color='red'>Address must be filled</font>");
			address=false;
		}
		else{
			address=true;
		}
		enable_submit();
	});
	$('#city').blur(function(){
		if(this.value==""){
			$('#err_city').html("<font color='red'>City must be filled</font>");
			city=false;
		}
		else{
			city=true;
		}
		enable_submit();
	});
	$('#phone').blur(function(){
		if(this.value==""){
			$('#err_phone').html("<font color='red'>Phone must be filled</font>");
			phone=false;
		}
		else{
			phone=true;
		}
		enable_submit();
	});

	$('#username').keypress(function(){
		$('#err_username').html("");
	});
	$('#email').keypress(function(){
		$('#err_email').html("");
	});
	$('#password').keypress(function(){
		$('#err_password').html("");
	});
	$('#conpassword').keypress(function(){
		$('#err_conpassword').html("");
	});
	$('#address').keypress(function(){
		$('#err_address').html("");
	});
	$('#city').keypress(function(){
		$('#err_city').html("");
	});
	$('#phone').keypress(function(){
		$('#err_phone').html("");
	});
	function enable_submit(){
		if(username && email && password && conpassword && address && city && phone){

			$('#register').removeAttr('disabled');
		}
		else{
			$('#register').attr('disabled','disabled');
		}
	}
	$('#register').click(function(){
		if(username && email && password && conpassword && address && city && phone){
			$.ajax({
				type: "POST",
				url: "<?php echo base_url(); ?>main/doregister",
				data: {
					username: $('#username').val(),
					email: $('#email').val(),
					password: $('#password').val(),
					gender: $('#gender').val(),
					birthday: $('#birthday').val(),
					address: $('#address').val(),
					city: $('#city').val(),
					phone: $('#phone').val(),

				},
				datatype:"json",
				success: function(data) {
					if(data.register.status == "Register Successful"){
						jSuccess('Register Successful<br>Please Login',
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
								window.location.replace("<?php echo base_url(); ?>");
							}
						});

					}
					else{
						jError(data.register.status,
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
	});
</script>