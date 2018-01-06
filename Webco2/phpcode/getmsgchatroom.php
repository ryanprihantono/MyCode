<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$roomid = $_POST['roomid'];
	$lastmsgid = $_POST["lastmsgid"];

	//$time = date("F j, Y, g:i a");

	$result = NULL;
	//echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";
	if($roomid != NULL && $lastmsgid != NULL){
		$result = mysql_query("select * from roommessage where roomid=$roomid and roommessageid>$lastmsgid");
		echo "<books>";
			//echo "<items>";		
				echo "<messageid>0</messageid>";
				echo "<message>dump</message>";
				echo "<sender>dump</sender>";
			//echo "</items>";
			//echo "<items>";		
				echo "<messageid>0</messageid>";
				echo "<message>dump</message>";
				echo "<sender>dump</sender>";
			//echo "</items>";
		while($data=mysql_fetch_array($result)){
			//echo "<items>";	
				echo "<messageid>".$data['roommessageid']."</messageid>";
				echo "<message>".$data['message']."</message>";
				echo "<sender>".$data['sender']."</sender>";
			//echo "</items>";	
		}
		
		echo "</books>";
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
		echo "<userlist>";
			$result2 = mysql_query("select * from trroom where roomid=$roomid");
			echo "<users>dump</users>";
			echo "<users>dump</users>";
			while($data2=mysql_fetch_array($result2)){
				echo "<users>".$data2['webcoid']."</users>";
			}
		echo "</userlist>";
	}
?>