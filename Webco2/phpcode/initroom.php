<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$roomid = $_POST['roomid'];


	$result = NULL;
	//echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";

	$result = mysql_query("select * from room where roomid=$roomid");
	if($data=mysql_fetch_array($result)){
		echo "<rm>".$data['rm']."</rm>";
		echo "<roomname>".$data['roomname']."</roomname>";
	}
	
		
	
?>