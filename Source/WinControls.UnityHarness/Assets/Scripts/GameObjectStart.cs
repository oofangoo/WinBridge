﻿using UnityEngine;
using System.Collections;
using WinControls;

public class GameObjectStart : MonoBehaviour {

    void Start()
    {
        reviewApp();
    }

	void Startt () {
		var messageBox = new MessageBox ();
		var trialUpgradeButton = new MessageBox.Command ("Trial Upgrade", trialUpgrade);
		var productPurchaseButton = new MessageBox.Command ("In app purchase", productPurchase);
		messageBox.ShowMessageBox ("Which scenario would you like to test?", "Store plugin test", trialUpgradeButton, productPurchaseButton);
	
		// Initialize the Store proxy on debug builds
		Store.DebugApp debugapp = new Store.DebugApp();
		debugapp.Name = "WinControls debug harness";
		debugapp.Price = 5.99;
		debugapp.IsTrial = false;
		debugapp.IsActive = true;

		Store.DebugProduct bigsword = new Store.DebugProduct();
		bigsword.ProductId = "bigsword";
		bigsword.Name = "really big swordy!";
		bigsword.Price = 99.99;

		Store.DebugProduct bigaxe = new Store.DebugProduct();
		bigaxe.ProductId = "bigaxe";
		bigaxe.Name = "really big axe!";
		bigaxe.Price = 65.99;
		bigaxe.IsActive = true;

		Store.EnableDebugWindowsStoreProxy (handleLicenseChanged, debugapp, bigsword, bigaxe);
	}

	void trialUpgrade(object UICommand) {
		Debug.LogError("Trial upgrade button clicked");
		Store.PurchaseFullApp (handlePurchase, Debug.isDebugBuild );
	}
	
	void productPurchase(object UICommand) {
		// Note: Full app must be active for in app product purchase to succeed
		// If there is a trial, it must be upgraded first
		Debug.LogError("In app purchase button clicked");
		Store.PurchaseProduct ("bigsword", handlePurchase, Debug.isDebugBuild );	
	}

	void handleLicenseChanged() {
		Debug.LogError ("License changed");
		Store.GetFullAppInfo (handleAppInfo, Debug.isDebugBuild);
		Store.GetProductInfo ("bigsword", handleProductInfo, Debug.isDebugBuild);
		Store.GetProductInfo ("bigaxe", handleProductInfo, Debug.isDebugBuild);
	}

	void handlePurchase(Store.PurchaseResult result) {
		Debug.LogError ("Purchase result " + result);
	}

	void handleAppInfo(Store.FullAppInfo info) {
		Debug.LogError ("App name " +  info.Name);
		Debug.LogError ("App price " +  info.Price);
		Debug.LogError ("Is app active? " + Store.IsFullAppActive (Debug.isDebugBuild));
	}

	void handleProductInfo(Store.ProductInfo info) {
		Debug.LogError ("Product name " +  info.Name);
		Debug.LogError ("Product price " +  info.Price);
		Debug.LogError ("Is " + info.Name + " product active? " + Store.IsProductActive (info.Id, Debug.isDebugBuild));
	}

    void reviewApp()
    {
        WinBridge.Store.RequestReview();
    }

	// Update is called once per frame
	void Update () {

	}
}
