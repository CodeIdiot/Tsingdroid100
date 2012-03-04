package org.codeidiot.cityadv.CityAdvUnityGame;

import android.content.Intent;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class CityAdvUnityPlayerActivity extends UnityPlayerActivity {
	
	private static final String TAG = "CityadvUnityPlayerActivity";
	private static final int REQUEST_CODE_QR_SCAN = 0;
	
	private boolean isGotoScan = false;
	
	public void startQrScan() {
		Intent intent = new Intent("com.google.zxing.client.android.SCAN");
		intent.setPackage("com.google.zxing.client.android");
		intent.putExtra("SCAN_MODE", "QR_CODE_MODE");
		
		isGotoScan = true;
        startActivityForResult(intent, REQUEST_CODE_QR_SCAN);
	}
	
	public void startNav() {
		Intent intent = new Intent("cn.edu.tsinghua.thu100guide.MAP_VIEW");
		intent.setPackage("cn.edu.tsinghua.thu100guide");
        startActivity(intent);
	}
	
	@Override
	protected void onPause() {
		super.onPause();
    	
		//if (!isGotoScan) {
			Intent intent = new Intent("com.processkeeper.status.back");
			sendBroadcast(intent);
		//}
	}
	
	@Override
	protected void onResume() {
		super.onResume();
		
		//if (!isGotoScan) {
	    	Intent intent = new Intent("com.processkeeper.status.front");
	    	sendBroadcast(intent);
		//}
	}
	
	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		super.onActivityResult(requestCode, resultCode, data);
		
		switch (requestCode) {
		case REQUEST_CODE_QR_SCAN:
			isGotoScan = false;
			if (resultCode == RESULT_OK) {
	            String contents = data.getStringExtra("SCAN_RESULT");
	            
	            UnityPlayer.UnitySendMessage("qrcodeButton", "OnScanResult", contents);
	            
	            Log.d(TAG, "Result: " + contents);
	        } else if (resultCode == RESULT_CANCELED) {
	        }
		}
	}
}
