<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];
	$sessionchatid = $_POST["sessionchatid"];

	$result = NULL;
	//echo "<satu>$webcoid</satu>";
	//echo "<dua>$sessionchatid</dua>";

	if($sessionchatid != "" && $sessionchatid!= NULL){
		$result = mysql_query("select * from message where chatsessionid=$sessionchatid and status='sent' and sender <> '$webcoid' and messagetypeid=1");
	
		echo "<books>";
		echo "<message>dump</message>";
		echo "<message>dump</message>";
		while($data=mysql_fetch_array($result)){
			//echo "<item>";
				echo "<message>".$data['message']."</message>";
				mysql_query("update message set status='read' where messageid=".$data['messageid']);
			//echo "</item>";
		}
		
		echo "</books>";
		
	}
	
?>