<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	
	$roomid = $_POST['roomid'];


	$result = NULL;
	echo "<satu>$roomid</satu>";
	//echo "<dua>$message</dua>";

	mysql_query("delete from roommessage where roomid=$roomid");
	mysql_query("delete from trroom where roomid=$roomid");
	mysql_query("delete from room where roomid=$roomid");
	
?>