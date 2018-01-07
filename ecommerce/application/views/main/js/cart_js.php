<script type="text/javascript">

var total = $("#total").val();
$(".action input").click(function() {
	var itemIDValSplitter 	= (this.id).split("_");
	var itemIDVal 			= itemIDValSplitter[1];
	var size_id				= $("#size_"+itemIDVal+"_true").val();
	var color_id			= $("#color_"+itemIDVal+"_true").val();
	/*if(size_id == null){
		size_id="0";
	}*/
	if(color_id == null){
		color_id="0";
	}
	$("#notificationsLoader").html('<img src="<?php echo base_url(); ?>assets/images/loader.gif">');

	if(color_id!="0"){
		$.ajax({
			type: "POST",
			url: "<?php echo base_url(); ?>main/add_to_cart",
			data: { itemID: itemIDVal, size:size_id,color:color_id,qty:$('#quantity_'+itemIDVal).val()},
			datatype:"json",
			success: function(response) {
				index = response.index;
				if( $("#cart_" + itemIDVal+"_"+index).length > 0){
					$("#cart_" + itemIDVal+"_"+index).animate({ opacity: 0 }, 500, function() {
						$("#cart_" + itemIDVal+"_"+index).before(response.theResponse).remove();
					});
					$("#cart_" + itemIDVal+"_"+index).animate({ opacity: 0 }, 500);
					$("#cart_" + itemIDVal+"_"+index).animate({ opacity: 1 }, 500);
				}
				else {
					$("#cart_item_wrap li:first").before(response.theResponse);
					$("#cart_item_wrap li:first").hide();
					$("#cart_item_wrap li:first").show("slow");
				}

				total += ($("#cart_"+itemIDVal+"_"+index)).val()*$('#quantity_'+itemIDVal).val();

				var newTotal = "";
				if(total>0){
					newTotal = "<li id='total' value='"+total+"'>Total : Rp. "+total+"</li>";
				}
				else{
					newTotal = "<li id='total' value='0'>Now <strong>0 item(s)</strong> in your cart</li>";
				}
				$("#total").animate({ opacity: 0 }, 500, function() {
					$("#total").before(newTotal).remove();
				});
				$("#total").animate({ opacity: 0 }, 500);
				$("#total").animate({ opacity: 1 }, 500);
				$("#notificationsLoader").empty();
			}
		});
	}
	else{
		jError("Please select color",
		{
			autoHide : false, // added in v2.0
			clickOverlay : true, // added in v2.0
			MinWidth : 250,
			TimeShown : 1000,
			ShowTimeEffect : 100,
			HideTimeEffect : 300,
			LongTrip :20,
			HorizontalPosition : 'center',
			VerticalPosition : 'top',
			ShowOverlay : true,
			ColorOverlay : '#000',
			OpacityOverlay : 0.3,
			onClosed : function(){ // added in v2.0

			},
			onCompleted : function(){ // added in v2.0

			}
		});
	}
});
$("#cart_item_wrap li").live("click", function(event) {

	var itemIDValSplitter 	= (this.id).split("_");
	var itemIDVal 			= itemIDValSplitter[1];
	var index 				= itemIDValSplitter[2];

	$("#notificationsLoader").html('<img src="/images/loader.gif">');


	$.ajax({
		type: "POST",
		url: "<?php echo base_url(); ?>main/delete_from_cart",
		data: { itemID: itemIDVal, idx:index},
		success: function(theResponse) {

			$("#cart_" + itemIDVal+"_"+index).hide("slow",  function() {$(this).remove();});

			total -= ($("#cart_"+itemIDVal+"_"+index)).val()*($("#qty_"+itemIDVal)).val();
			//alert(($("#cart_"+itemIDVal)).val()+"-"+($("#qty_"+itemIDVal)).val());
			var newTotal = "";
			if(total>0){
				newTotal = "<li id='total' value='"+total+"'>Total : Rp. "+total+"</li>";
			}
			else{
				newTotal = "<li id='total' value='0'>Now <strong>0 item(s)</strong> in your cart</li>";
			}
			$("#total").animate({ opacity: 0 }, 500, function() {
				$("#total").before(newTotal).remove();
			});
			$("#total").animate({ opacity: 0 }, 500);
			$("#total").animate({ opacity: 1 }, 500);
			$("#notificationsLoader").empty();

		}
	});

});

/*$(".size select").mouseover(function() {
	var itemIDValSplitter 	= (this.id).split("_");
	var table				= itemIDValSplitter[0];
	var itemIDVal 			= itemIDValSplitter[1];
	var isLoaded			= itemIDValSplitter[2];

	get_size(itemIDVal,isLoaded);

});*/
$(".color select").mouseover(function() {
	var itemIDValSplitter 	= (this.id).split("_");
	var table				= itemIDValSplitter[0];
	var itemIDVal 			= itemIDValSplitter[1];
	var isLoaded			= itemIDValSplitter[2];

	get_color(itemIDVal,isLoaded);

});
/*
function get_size(item_id,isLoaded){
	if(isLoaded=="false"){
		$.getJSON('<?php echo base_url().'main/get_size'; ?>/'+item_id,function(data){
			$("#size_"+item_id+"_false").before("<select id='size_"+item_id+"_true'><option value='0'>-Size-</option></select>").remove();
			$.each(data.sizes,function(i,key){
				$("#size_"+item_id+"_true").append(new Option(key.size, key.size_id));
			});
		});
	}
}*/
function get_color(item_id,isLoaded){
	if(isLoaded=="false"){
		$.getJSON('<?php echo base_url().'main/get_color'; ?>/'+item_id,function(data){
			$("#color_"+item_id+"_false").before("<select id='color_"+item_id+"_true'><option value='0'>-Color-</option></select>").remove();
			$.each(data.colors,function(i,key){
				$("#color_"+item_id+"_true").append(new Option(key.color,key.color_id));
			});
		});
	}
}

$('#checkout').click(function(){
	var user = '<?php echo $this->session->userdata('username'); ?>';
	if(user=='' || user=='false'){
		jError("Please login first before Check Out",
		{
			autoHide : false, // added in v2.0
			clickOverlay : true, // added in v2.0
			MinWidth : 250,
			TimeShown : 1000,
			ShowTimeEffect : 100,
			HideTimeEffect : 300,
			LongTrip :20,
			HorizontalPosition : 'center',
			VerticalPosition : 'top',
			ShowOverlay : true,
			ColorOverlay : '#000',
			OpacityOverlay : 0.3,
			onClosed : function(){ // added in v2.0

			},
			onCompleted : function(){ // added in v2.0

			}
		});
	}
	else{
		$.ajax({
			type: "POST",
			url: "<?php echo base_url(); ?>main/checkout",
			data: { user_id:'<?php echo $this->session->userdata('user_id');  ?>',remark:$('#remark').val(),total:$('#total').val()},
			datatype:"json",
			success: function(response) {
				if(response.status=="success"){
					jSuccess("Check Out Success<br>Please confirm your Payment",
					{
						autoHide : false, // added in v2.0
						clickOverlay : true, // added in v2.0
						MinWidth : 250,
						TimeShown : 1000,
						ShowTimeEffect : 100,
						HideTimeEffect : 300,
						LongTrip :20,
						HorizontalPosition : 'center',
						VerticalPosition : 'top',
						ShowOverlay : true,
						ColorOverlay : '#000',
						OpacityOverlay : 0.3,
						onClosed : function(){ // added in v2.0

						},
						onCompleted : function(){ // added in v2.0
							window.location.replace("<?php echo base_url(); ?>main/payment/"+response.cart_id);
						}
					});
				}
				else{
					jError("Check Out Failure",
					{
						autoHide : false, // added in v2.0
						clickOverlay : true, // added in v2.0
						MinWidth : 250,
						TimeShown : 1000,
						ShowTimeEffect : 100,
						HideTimeEffect : 300,
						LongTrip :20,
						HorizontalPosition : 'center',
						VerticalPosition : 'top',
						ShowOverlay : true,
						ColorOverlay : '#000',
						OpacityOverlay : 0.3,
						onClosed : function(){ // added in v2.0

						},
						onCompleted : function(){ // added in v2.0

						}
					});
				}
			}
		});
	}
});
//detail transaction
<?php if($mainbar=='detail_transaction'){ ?>
$(document).ready(function() {
	var oTable = $('#dataproduct').dataTable();
} );
<?php } ?>

//payment
$('#submit').click(function(){
	if($("#bank").val()!="" && $('#payment_account').val()!="" && $('#account_name').val()!='' && $('#amount_paid')!='' && $('#date').val()!=''){
		var bank_splitter	= ($("#bank").val()).split("_");
		
		var cart_id_val			= $('#cart_id').val();
		var date_val			= $('#date').val();
		var amount_paid_val		= $('#amount_paid').val();
		var bank_id_val			= bank_splitter[1];
		var account_name_val	= $('#account_name').val();
		var payment_account_val = $('#payment_account').val();
		$.ajax({
			type: "POST",
			url: "<?php echo base_url(); ?>main/dopayment",
			data: { 
				cart_id 		: cart_id_val,
				date 			: date_val,
				amount_paid		: amount_paid_val,
				bank_id			: bank_id_val,
				account_name	: account_name_val,
				payment_account	: payment_account_val
			},
			datatype:"json",
			success: function(response) {
				if(response.status=="success"){
					success(response.msg);
				}
				else{
					failed(response.msg);
				}
			}
		});
	}
	else{
		failed('All transaction field must be fille');
	}
	
});
function success(msg){
	jSuccess(msg,
	{
		autoHide : false, // added in v2.0
		clickOverlay : true, // added in v2.0
		MinWidth : 250,
		TimeShown : 1000,
		ShowTimeEffect : 100,
		HideTimeEffect : 300,
		LongTrip :20,
		HorizontalPosition : 'center',
		VerticalPosition : 'top',
		ShowOverlay : true,
		ColorOverlay : '#000',
		OpacityOverlay : 0.3,
		onClosed : function(){ // added in v2.0

		},
		onCompleted : function(){ // added in v2.0
			window.location.replace('<?php echo base_url(); ?>main/payment_confirmation');
		}
	});
}
function failed(msg){
	jError(msg,
	{
		autoHide : false, // added in v2.0
		clickOverlay : true, // added in v2.0
		MinWidth : 250,
		TimeShown : 1000,
		ShowTimeEffect : 100,
		HideTimeEffect : 300,
		LongTrip :20,
		HorizontalPosition : 'center',
		VerticalPosition : 'top',
		ShowOverlay : true,
		ColorOverlay : '#000',
		OpacityOverlay : 0.3,
		onClosed : function(){ // added in v2.0

		},
		onCompleted : function(){ // added in v2.0

		}
	});
}
</script>