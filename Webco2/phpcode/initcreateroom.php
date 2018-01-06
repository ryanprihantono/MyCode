<?php
	echo "<?xml version=\"1.0\" ?>\n";
	include("connect.php");
	mysql_select_db("webcodb");
	
	
	
	$room = $_POST['room'];
	
	$lastroomid = 0;
	//echo "<satu>$sessionchatid</satu>";
	//echo "<dua>$message</dua>";

	echo "<books>";
	$result = mysql_query("select * from roomcategory");
	while($data = mysql_fetch_array($result)){
		echo "<roomcategory>".$data['roomcategory']."</roomcategory>";
	}
	
	//echo "<hasil>".mysql_affected_rows($result)."</hasil>";
	echo "</books>";

?>