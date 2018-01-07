<script type="text/javascript">
	var username = false;
	var password = false;
	$('#login_username').blur(function (){
		if(this.value == ""){
			$('#err_login').html("Username must be filled");
			username = false;
		}
		else{
			username = true;
		}
	});
	$('#login_password').blur(function (){
		if(this.value == ""){
			$('#err_login').html("Password must be filled");
			password = false;
		}
		else{
			password = true;
		}
	});
	$('#login_username').keypress(function (e){
		if(this.value == ""){
			$('#err_login').html("Username must be filled");
			username = false;
		}
		else{
			$('#err_login').html("");
			username = true;
			var code = (e.keyCode ? e.keyCode : e.which);
			if(code == 13) { 
				login();
			}
		}
	});
	$('#login_password').keypress(function (e){
		if(this.value == ""){
			$('#err_login').html("Password must be filled");
			password = false;
		}
		else{
			$('#err_login').html("");
			password = true;
			var code = (e.keyCode ? e.keyCode : e.which);
			if(code == 13) { 
				login();
			}
		}
	});
	$('#login').click(function(){
		login();
	});

	function login(){
		if(username && password){
			$.ajax({
				type: "POST",
				url: "<?php echo base_url(); ?>main/dologin",
				data: {
					username: $('#login_username').val(),
					password: $('#login_password').val()
				},
				datatype:"json",
				success: function(data) {
					if(data.login.status == "Login Successful"){
						jSuccess('Login Successful',
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
								if('<?php echo $mainbar; ?>' != 'mycart')
									window.location.replace('<?php echo base_url(); ?>main');
								else
									window.location.replace('<?php echo base_url(); ?>main/mycart');
							},
							onCompleted : function(){ // added in v2.0
								$('#sidebar-login').before("<div id='sidebar-login'>Welcome, "+data.login.username+"<br><br><a href='<?php echo base_url().'main/dologout'; ?>' style='color:#000'><button type='submit' class='uniform' id='logout'>Logout</button></a></div><div style='margin-top:5px'><a href='<?php echo base_url().'main/profile'; ?>' style='text-decoration:none;color:black'>My Profile <img src='<?php echo base_url().'assets/admin/images/icon/setting.png'; ?>' title='Edit Profile' /></a></div>").remove();
								/*
								<div class='sidebar-box'><h1 class='sidebar-head'>My Profile <a href=''> <img src='' title='Edit Profile' /></a></h1><div class='sidebar-content'>
								<div></div>
								*/
							}
						});

					}
					else{
						$('#err_login').html(data.login.status);
					}
				}
			});
		}
		else{
			$('#err_login').html("Invalid Username or Password");
		}
	}
</script>