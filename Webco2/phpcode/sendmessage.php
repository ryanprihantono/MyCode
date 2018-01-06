<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST['webcoid'];
	$sessionchatid = $_POST["sessionchatid"];
	$message = $_POST["message"];
	$time = date("F j, Y, g:i a");

	$result = NULL;
	echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";
	if($sessionchatid != "" && $sessionchatid != NULL){
		$result = mysql_query("insert into message (message,time,status,chatsessionid,sender,messagetypeid) values ('$message','$time','sent','$sessionchatid','$webcoid',1)");
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
		
	}
?>