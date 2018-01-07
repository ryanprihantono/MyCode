<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><?php echo $web_title; ?></title>
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>asset/css/login.css" />
<script type="text/javascript" src="<?php echo base_url(); ?>asset/js/jquery.js"></script>
</head>
<body>
  <div id="jj-logo">
    <font size="+2" color="#FF0000">Sistem </font><font size="+2">Inventory </font> <font size="+3" color="#0000FF">FTI </font><font size="+2">UKSW</font>
  </div>
  <hr size="5px" class="line-blue"/>
  <hr size="2px" class="line-blue"/>
    <form name="frmLogin" method="post" action="<?php echo site_url('login/doLogin'); ?>" class="round5">
      <h1>Login</h1>
      <div>
        <label>Username</label>
        <input type="text" name="txtusername" class="jj-input" />
      </div>
      <div>
        <label>Password</label>
        <input type="password" name="txtpassword" class="jj-input" />
      </div>
      <div>
		<?php
        if(isset($_REQUEST['err'])){
		?>
        <p class="err">
        	<?php echo $_REQUEST['err']; ?>
        </p>
        <?php
        }
        ?>
      </div>
      <div>
        <button type="submit" name="submit" class="button round5">Login</button>
      </div>
    </form>
</body>
</html>