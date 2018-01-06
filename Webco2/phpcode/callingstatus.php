<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid1 = $_POST["webcoid1"];
	$webcoid2 = $_POST["webcoid2"];
	$sender = $webco1;
	$sessionchatid = "";
	$messageid = $_POST["messageid"];

	$result = mysql_query("select * from chatsession where webcoid1='$webcoid1' && webcoid2='$webcoid2'");
	$result2 = mysql_query("select * from chatsession where webcoid1='$webcoid2' && webcoid2='$webcoid1'");
	
	if($data=mysql_fetch_array($result)){
		$sessionchatid = $data['chatsessionid'];
	}
	else if($data2=mysql_fetch_array($result2)){
		$sessionchatid = $data2['chatsessionid'];
	}
	echo "<satu>select * from message where chatsessionid=$sessionchatid and messagetypeid=2</satu>";
	$result3 = mysql_query("select * from message where chatsessionid=$sessionchatid and messagetypeid=2");
	if($data3=mysql_fetch_array($result3)){
		echo "<status>calling</status>";
		echo "<sender>".$data3['sender']."</sender>";
		echo "<messageid>".$data3['messageid']."</messageid>";
	}
	else{
		$result4 = mysql_query("select * from message join messagetype on message.messagetypeid=messagetype.messagetypeid where messageid=$messageid");
		if($data2=mysql_fetch_array($result4)){
			echo "<status>".$data2['messagetype']."</status>";
			echo "<sender>".$data2['sender']."</sender>";
		}
	}

	//echo "<satu>insert into roommessage (message,sender,time,roomid) values ('$message','$sender','$time',$roomid)</satu>";
	//echo "<dua>$message</dua>";
	
	//$result = mysql_query("insert into roommessage (message,sender,time,roomid) values ('$message','$sender','$time',$roomid)");
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
		
	
?>