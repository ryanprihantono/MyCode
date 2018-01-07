<?php
	$timezone = "Asia/Jakarta";
	if(function_exists('date_default_timezone_set')) date_default_timezone_set($timezone);
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Kent Fashion House - <?php echo $page_title; ?></title>
<link rel="shortcut icon" href="<?php echo base_url(); ?>assets/main/images/logo/icon-strukturpintar.png" />
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>assets/admin/css/base.css" />
<?php

foreach($css_files as $file): ?>
	<link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />
<?php endforeach; ?>
<?php foreach($js_files as $file): ?>
	<script src="<?php echo $file; ?>"></script>
<?php endforeach; ?>
</head>
<body>
<div id="admin-layout">
  <div id="admin-info">
    <ul>
    	<li>
        	Welcome, <?php echo $this->session->userdata('username');?>
        </li>
    	<li>
        	<a href="<?php echo base_url().'admin/changePassword/edit/'.$this->session->userdata('userid'); ?>" class="span">Change Password</a>
        </li>
        <li style="border-right: none;">
        	<a href="<?php echo base_url().'login/dologout'; ?>" class="span">Logout</a>
        </li>
  	</ul>
  </div>
  <div class="clear"></div>
  <div id="admin-navigation">
    <div id="admin-mainmenu">
        <ul>
			<li><a href="<?php echo base_url().'admin/mainmenu/3'; ?>" id="icon-admin"><img src="<?php echo base_url();?>assets/admin/images/logo/icon.png" height="25" /></a></li>
            <li><a href="<?php echo base_url().'admin/mainmenu/1'; ?>">Products and Master Table</a></li>
            <li><a href="<?php echo base_url().'admin/mainmenu/2'; ?>">Settings</a></li>
            <li><a href="<?php echo base_url().'admin/mainmenu/3'; ?>">Transactions</a></li>
        </ul>
    </div>
  </div>
  <div class="clear"></div>
  <div id="admin-sidebar">
      <?php include "sidebar/$sidebar.php"; ?>
  </div>
  <div id="admin-container">
    <div id="admin-title"><?php echo $page_title; ?></div>
    <div id="admin-content" class="round-bottom">
      <?php include "content/$page_name.php"; ?>
    </div>
    <div id="admin-footer">
    	&copy; Copyright 2012. Website Name. Powered By PT. SAYINDO INDONESIA
    </div>
  </div>
</div>
<div class="clear"></div>
</body>
</html>
