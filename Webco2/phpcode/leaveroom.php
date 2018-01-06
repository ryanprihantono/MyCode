<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];
	$roomid = $_POST['roomid'];
	

	$result = NULL;
	echo "<satu>$roomid</satu>";
	//echo "<dua>$message</dua>";

	mysql_query("delete from trroom where webcoid='$webcoid'");
	
?>