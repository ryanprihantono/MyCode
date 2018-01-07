<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Kent Fashion House - <?php echo $web_title; ?></title>
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>assets/admin/css/login.css" />
<script type="text/javascript" src="<?php echo base_url(); ?>assets/admin/js/jquery.js"></script>
</head>

<body>
<div id="admin-logo">
	<img src="<?php echo base_url(); ?>assets/admin/images/logo/logo.png" />
</div>
<form name="frmLogin" method="post" action="<?php echo base_url()."login/dologin"; ?>" class="round5">
	<h1>Login</h1>
    <div>
        <label>Username</label>
        <input type="text" name="username" class="admin-input" />
    </div>
    <div>
        <label>Password</label>
        <input type="password" name="password" class="admin-input" />
    </div>
    <div>
    	<?php
        	if(isset($_GET['err'])){
				echo "<p class=\"err\">".$_GET['err']."</p>";
			}
		?>
    </div>
    <div>
        <button type="submit" name="submit" class="button round5">Login</button>
    </div>
</form>
</body>
</html>