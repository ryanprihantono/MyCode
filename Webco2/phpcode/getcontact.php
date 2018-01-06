<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];

	$result = NULL;
	if($webcoid != ""){
		$result = mysql_query("select * from friendship where webcoid='$webcoid'");
		$result2 = mysql_query("select * from friendship where friendid='$webcoid'");
		echo "<books>";
		while($data=mysql_fetch_array($result)){
			//echo "<item>";
				echo "<friend>".$data['friendid']."</friend>";
			//echo "</item>";
		}
		while($data2=mysql_fetch_array($result2)){
			//echo "<item>";
				echo "<friend>".$data2['webcoid']."</friend>";
			//echo "</item>";
		}
		echo "</books>";
	}
?>