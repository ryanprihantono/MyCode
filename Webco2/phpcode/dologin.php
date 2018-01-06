<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];
	$password = $_POST["password"];
	$password = md5($password);
	$result = NULL;
	if($webcoid != "" && $password != ""){
		$result = mysql_query("select * from user where webcoid='$webcoid' and password='$password'");
	}
	echo "<satu>select * from user where webcoid='$webcoid' and password='$password'</satu>";
	if($data = mysql_fetch_array($result)){
		echo "<session>$webcoid</session>";
	}
	else
		echo "<session></session>";
?>