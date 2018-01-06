<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	


	$result = NULL;
	//echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";
	$result2 = mysql_query("select * from roomcategory");
	
	$result = mysql_query("select * from room join roomcategory on room.roomcategoryid=roomcategory.roomcategoryid");
	echo "<books>";
	/*echo "<items>";
		echo "<roomid>0</roomid>";
		echo "<roomname>dump</roomname>";
		echo "<roomcategory>dump</roomcategory>";
	echo "</items>";
	echo "<items>";
		echo "<roomid>0</roomid>";
		echo "<roomname>dump</roomname>";
		echo "<roomcategory>dump</roomcategory>";
	echo "</items>";*/
	while($data=mysql_fetch_array($result)){
		echo "<items>";
			echo "<roomid>".$data['roomid']."</roomid>";
			echo "<roomname>".$data['roomname']."</roomname>";
			echo "<roomcategory>".$data['roomcategory']."</roomcategory>";
		echo "</items>";
	}
	echo "</books>";
	echo "<cats>";
	while($data2=mysql_fetch_array($result2)){
		echo "<category>".$data2['roomcategory']."</category>";
	}
	echo "</cats>"
		
	
?>