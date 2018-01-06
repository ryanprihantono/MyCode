<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$sender = $_POST["sender"];
	$roomid = $_POST["roomid"];
	$message = $_POST["message"];
	$time = date("F j, Y, g:i a");

	$result = NULL;
	echo "<satu>insert into roommessage (message,sender,time,roomid) values ('$message','$sender','$time',$roomid)</satu>";
	//echo "<dua>$message</dua>";
	
	$result = mysql_query("insert into roommessage (message,sender,time,roomid) values ('$message','$sender','$time',$roomid)");
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
		
	
?>