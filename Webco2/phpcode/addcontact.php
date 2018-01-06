<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	$webcoid = $_POST["webcoid"];
	$webcoid2 = $_POST["friendid"];
	
	$result = NULL;
	
	if($webcoid != "" && $webcoid2 != ""){
		$result2=mysql_query("select * from friendship where webcoid='$webcoid' and friendid='$webcoid2'");
		$result3=mysql_query("select * from friendship where webcoid='$webcoid2' and friendid=$webcoid'");
		if(mysql_num_rows($result2)>0 && mysql_num_rows($result3)==0){
			if($data1=mysql_fetch_array($result2)){
				$message = $data1['statusid'];
				echo "<message>$message</message>";
			}

		}
		else if(mysql_num_rows($result3)==0 && mysql_num_rows($result2)>0){
			if($data2=mysql_fetch_array($result3)){
				$message = $data2['statusid'];
				echo "<message>$message</message>";
			}

		}
		else{
			if(mysql_query("insert into friendship values('$webcoid','$webcoid2',1)")){
				echo "<message>Success</message>";
			}
		}
/*
		else{
			echo "<message>Webco id not found</message>";
		}*/
	}
	
?>