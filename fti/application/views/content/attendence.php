<style>
#filter-form {
	width: 50%;
	font-size: 16px;
	border: 1px #FFF solid;
	padding: 0px;
	margin: 0 auto 0 auto;
}

#filter-form div {
	width: 100%;
}

#filter-form div.field-bar {
	background: #FFF;
	height: 30px;
}

#filter-form div.field-bar-even {
	background: #EFEFEF;
	height: 30px;
}

#filter-form div label {
	width: 30%;
	padding: 5px;
	float:left;
}

#filter-form div input {
	width: 60%;
	padding: 5px;
	float:left;
	-moz-border-radius:2px;
	-webkit-border-radius:2px;
	-khtml-border-radius:2px;
	border-radius:2px;
	border: 1px solid #999;
}

#filter-form div input:hover {
	border: 1px solid #333;
}

#filter-form div input:focus {
	border: 1px solid #000;
}

#filter-form .btn{
	border: 1px solid #000;
	height: 20px;
	cursor: pointer;
	margin: 5px;
	float: right;
}

</style>
<div id="filter-form" class="round5">
  <form action="<?php echo site_url('main/convert_absensi'); ?>" method="post">
  <div class="field-bar">
      <label>Date</label>
      <input type="text" name="tanggal" value="" class='datepicker'/>
  </div>
  <div class="clear"></div>
  <input type="submit" value="Update From Access Database" class="btn"/>
  <div class="clear"></div>
  </form>
<?php
	if(isset($_GET['err'])){
		echo "<p class=\"err\">".$_GET['err']."</p>";	
	}
?>

</div>
<div class="clear"></div>
<?php
if(isset($output))
	echo $output;
else{
}