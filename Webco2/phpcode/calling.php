<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid1 = $_POST["webcoid1"];
	$webcoid2 = $_POST["webcoid2"];
	$messageid = $_POST["messageid"];
	$sender = "";
	$sessionchatid = "";
	$status = $_POST["status"];
	$time = date("F j, Y, g:i a");
	
	if($status == "2"){
		$sender = $webcoid1;
		$result = mysql_query("select * from chatsession where webcoid1='$webcoid1' && webcoid2='$webcoid2'");
		$result2 = mysql_query("select * from chatsession where webcoid1='$webcoid2' && webcoid2='$webcoid1'");
		
		if($data=mysql_fetch_array($result)){
			$sessionchatid = $data['chatsessionid'];
		}
		else if($data=mysql_fetch_array($result2)){
			$sessionchatid = $data['chatsessionid'];
		}
		else{
			mysql_query("insert into chatsession (webcoid1,webcoid2) values($webcoid1,$webcoid2)");
			$result3 = mysql_query("select * from chatsession where webcoid1='$webcoid1' and webcoid2='$webcoid2'");
			if($data2=mysql_fetch_array($result3)){
				$sessionchatid = $daata2['chatsessionid'];
			}
		}
		mysql_query("insert into message (message,time,status,chatsessionid,sender,messagetypeid) values ('Video Call','$time','sent',$sessionchatid,'$sender',$status)");
		$resul4 = mysql_query("select * from message where chatsessionid=$sessionchatid and sender='$sender' and time='$time' and messagetypeid=2");
		if($data3=mysql_fetch_array($resul4)){
			echo "<messageid>".$data3['messageid']."</messageid>";
		}
	}
	else{
		mysql_query("update message set messagetypeid=$status where messageid=$messageid");
	}
	
	$result = NULL;
	echo "<satu>a</satu>";
	//echo "<dua>$message</dua>";
	
	$result = mysql_query("insert into roommessage (message,sender,time,roomid) values ('$message','$sender','$time',$roomid)");
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
		
	
?>