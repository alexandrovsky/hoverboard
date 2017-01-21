//
//	  UnityOSC - Example of usage for OSC receiver
//
//	  Copyright (c) 2012 Jorge Garcia Martin
//
// 	  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// 	  documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// 	  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
// 	  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// 	  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// 	  of the Software.
//
// 	  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// 	  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// 	  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// 	  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// 	  IN THE SOFTWARE.
//

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class oscControl : MonoBehaviour {

	[Range(1, 100)]
	public int lowPassFilterLength = 50;

	[Range(0.0f, 1.0f)]
	public float dryWet = 0.5f;

	public String touchOscIp = "134.102.150.174";



	Queue<Vector3> lpfQueue = new Queue<Vector3>();


	public Vector3 rotation = Vector3.zero;


	private Dictionary<string, ServerLog> servers;
	
	// Script initialization
	void Start() {	
		OSCHandler.Instance.Init("hoverboard", 3333, "phone", touchOscIp, 9000); //init OSC
		servers = new Dictionary<string, ServerLog>();
	}

	// NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
	void Update() {
		
		OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
		
	    foreach( KeyValuePair<string, ServerLog> item in servers )
		{
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if(item.Value.log.Count > 0) 
			{
				int lastPacketIndex = item.Value.packets.Count - 1;
				for(int i = 0; i < item.Value.packets[lastPacketIndex].Data.Count; i++){
//					UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} IDX: {2} VALUE: {3}", 
//						item.Key, // Server name
//						item.Value.packets[lastPacketIndex].Address, // OSC address
//						i,
//						item.Value.packets[lastPacketIndex].Data[i].ToString())); // value
				}

				if(item.Value.packets[lastPacketIndex].Address == "/accxyz"){
					rotation.x = (float)item.Value.packets[lastPacketIndex].Data[1];
					rotation.z = (float)item.Value.packets[lastPacketIndex].Data[0];

					rotation = LowPassFilter(rotation);
//					rotation.x *= 60;
//					rotation.z *= -60;
				}

			}
	    }
	}


	public void Vibrate(){
		foreach(KeyValuePair<string, ClientLog> client in OSCHandler.Instance.Clients){
			OSCMessage message = new OSCMessage("/vibrate");
			client.Value.client.Send(message);
		}
	}

	Vector3 LowPassFilter(Vector3 input){

		lpfQueue.Enqueue(input);
		if(lpfQueue.Count > lowPassFilterLength){
			lpfQueue.Dequeue();
		}

		Vector3 avg = Vector3.zero;
		foreach(Vector3 v in lpfQueue){
			avg += v;
		}

		avg /= lowPassFilterLength;

		Vector3 output = Vector3.Lerp(input, avg, dryWet);

		return output;

	}

}