<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");

	$rm = $_POST['rm'];
	$roomname = $_POST['roomname'];
	$category = $_POST['category'];
	$type = $_POST['type'];
	$password = $_POST['password'];
	$room = $_POST['room'];
/*
	$rm = "ryan";
	$roomname = "asdf";
	$category = 1;
	$type = 1;
	$password = "";
	$room = "chat";
*/
	$result = NULL;
	//echo "<satu>insert into room (roomname,roomtypeid,roomcategoryid,password,room) values ('$roomname',$type,$category,'$password','$room')</satu>";
	//echo "<dua>$message</dua>";
	
	$result3 = mysql_query("select * from room where rm='$rm'");
	if(mysql_num_rows($result3)<1){
		echo "<message></message>";
		$query = "insert into room (roomname,roomtypeid,roomcategoryid,room,rm) values ('$roomname',$type,$category,'$room','$rm')";
		if($password!="" && $password!=NULL){
			$query="insert into room (roomname,roomtypeid,roomcategoryid,password,room,rm) values ('$roomname',$type,$category,'$password','$room','$rm')";
			
		}
		$result = mysql_query($query);

		$result2 = mysql_query("SELECT * FROM room where rm='$rm'");
		if($data2=mysql_fetch_array($result2)){
			echo "<roomid>".$data2['roomid']."</roomid>";
			mysql_query("insert into trroom values(".$data2['roomid'].",'$rm')");
		}
			

	}
	else{ 
		echo "<roomid></roomid>";
		echo "<message>Can not create more than a room</message>";
	}
		//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
?>