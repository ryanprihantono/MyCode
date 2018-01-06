<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];
	$password = $_POST["password"];
	$email = $_POST["email"];
	$password = md5($password);
	$result = NULL;
	if($webcoid != "" && $password != "" && $email != ""){
		$result = mysql_query("insert into user (webcoid,password,email,usertypeid) values('$webcoid','$password','$email',1)");
		//
	}
	//echo "insert into user (webcoid,password,email) values ('$webcoid','$password','$email')";
	if($result>0){
		echo "<status>true</status>";
	}
	else
		echo "<status>false</status>";
?>