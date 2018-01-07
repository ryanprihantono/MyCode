<h1 class="head-mainbar round5">Payment Confirmation</h1>
<?php echo $payment; ?>
<script type="text/javascript">
	function _back(){
		window.location.replace("<?php echo base_url(); ?>main/profile");
	}
	function gen_bank(){
		var splitter 			= ($('#bank').val()).split("_");
		var bank_account		= splitter[0];
		var bank_id				= splitter[1];
		$('#bank_account').html(bank_account);
		$('#bank_id').val(bank_id);
	}
	
</script>