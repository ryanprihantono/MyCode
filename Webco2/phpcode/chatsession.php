<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid1 = $_POST["webcoid1"];
	$webcoid2 = $_POST["webcoid2"];

	$result = NULL;
	if($webcoid1 != "" && $webcoid2 != "" && $webcoid1 != NULL && $webcoid2 != NULL){
		$result = mysql_query("select * from chatsession where webcoid1='$webcoid1' && webcoid2='$webcoid2'");
		$result2 = mysql_query("select * from chatsession where webcoid1='$webcoid2' && webcoid2='$webcoid1'");
		//echo "<books>";
		if($data=mysql_fetch_array($result)){
			//echo "<item>";
				echo "<chatsessionid>".$data['chatsessionid']."</chatsessionid>";
			//echo "</item>";
		}
		if($data2=mysql_fetch_array($result2)){
			//echo "<item>";
				echo "<chatsessionid>".$data2['chatsessionid']."</chatsessionid>";
			//echo "</item>";
		}
		if(mysql_num_rows($result)==0 && mysql_num_rows($result2)==0){
			mysql_query("insert into chatsession (webcoid1,webcoid2) values('$webcoid1','$webcoid2')");
			$result3 = mysql_query("select * from chatsession where webcoid1='$webcoid1' && webcoid2='$webcoid2'");
			if($data3=mysql_fetch_array($result3)){
				echo "<chatsessionid>".$data['chatsessionid']."</chatsessionid>";
			}
		}
		
		//echo "</books>";
	}
	//else
		//echo "<sessionchatid>".$webcoid1."-".$webcoid2."</sessionchatid>";
?>