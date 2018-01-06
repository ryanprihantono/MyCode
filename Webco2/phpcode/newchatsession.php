<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];

	$result = NULL;
	if($webcoid != "" && $webcoid!= NULL){
		$result = mysql_query("select * from chatsession where webcoid1='$webcoid' or webcoid2='$webcoid'");
		echo "<books>";
		while($data=mysql_fetch_array($result)){
			//echo "<item>";
				$result2 = mysql_query("select * from message where chatsessionid=".$data['chatsessionid']." and status='sent' and sender <> '$webcoid' and messagetypeid<3");
				if(mysql_num_rows($result2)>0){
					echo "<from>".$data['webcoid1']."</from>";
					echo "<from>".$data['webcoid2']."</from>";
				}
			//echo "</item>";
		}
		echo "</books>";
	}
?>