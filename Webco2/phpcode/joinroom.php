<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST['webcoid'];
	$roomid = $_POST['roomid'];


	$result = NULL;
	//echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";

	$result = mysql_query("select * from room where roomid=$roomid");
	if($data=mysql_fetch_array($result)){
		echo "<room>".$data['room']."</room>";
		mysql_query("insert into trroom values($roomid,'$webcoid')");
		echo "<message></message>";
	}
	else{
		echo "<message>Room not found</message>";
	}
	
		
	
?>