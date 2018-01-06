var current='';
var current_service='';
var current_cetak='';
var service_id='5';
var service_flag='';

function resetservice()
{
	 $('.rollover2').each(function(i) {
		service_id='5';
		service_flag='';
		$(this).attr('src','/img/'+$(this).attr('title').replace('-on',''));
	});
}

$(function(){
	$('#cetak').click(function(){
		var ok=true;
		var nomorpolisi=$('#nomorpolisi').val();
		var tipe=$('#tipe').val();
		var merk=$('#merk').val()

		if(nomorpolisi=='')
		{
			alert('Nomor Polisi belum dimasukkan. Silahkan masukkan dahulu');
			$('#nomorpolisi').focus();
			ok=false;
		}
		else if(merk=='')
		{
			alert('Merk belum dimasukkan. Silahkan masukkan dahulu');
			$('#merk').focus();
			ok=false;
		}
		else if(tipe=='')
		{
			alert('Tipe belum dimasukkan. Silahkan masukkan dahulu');
			$('#tipe').focus();
			ok=false;
		}
		if(ok)
		{
			if(confirm('Apakah Anda yakin Nomor Polisi yang dimasukkan sudah benar?\nJika ya, Tekan OK.'))
			{
				var options = { 
					url:        '/main/cetaktiket/'+service_id+'/'+$('#nomorpolisi').val()+'/'+$('#merk').val()+'/'+$('#tipe').val(), 
					success:function(data){						
						if(data=='404')
						{
							alert('Terdapat kesalahan program, silahkan hubungi Programmer.');
						}
						else if(data=='201')
						{
							alert('Nomor Polisi atau meja masih ada di pending list pada form kasir, silahkan cek lagi.');
						}						
						resetservice();
						$('#nomorpolisi').val('');
						$('#merk').val('');
						$('#tipe').val('');
					} 
				}; 
				$('#formcetak').ajaxSubmit(options); 
				alert('Sedang Mencetak Kertas...\nTEKAN OK JIKA KERTAS SELESAI DICETAK.');
			}
		}
	});

	$('#cetaknota').hover(
		function(){
			current_cetak_nota=$(this).attr('src');
			$(this).attr('src','/img/'+$(this).attr('title'));
		},
		function()
		{
			$(this).attr('src',current_cetak_nota);
		}
	);

	$('#cetak').hover(
		function(){
			current_cetak=$(this).attr('src');
			$(this).attr('src','/img/'+$(this).attr('title'));
		},
		function()
		{
			$(this).attr('src',current_cetak);
		}
	);

	$('.rollover2').click(function(){
		resetservice();
		whatclick=$(this).attr('id');
		service_id=whatclick.replace('service','');
		$(this).attr('src','/img/'+$(this).attr('title'));
		service_flag=whatclick;
	});

	$('.rollover2').hover(
		function(){
			current_service=$(this).attr('src');
			$(this).attr('src','/img/'+$(this).attr('title'));
		},
		function()
		{
			if(service_flag!=$(this).attr('id'))
			{
				$(this).attr('src',current_service);
			}
		}
	);

});