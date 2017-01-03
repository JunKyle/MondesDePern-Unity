using UnityEngine;

public class UIDialog {

	public delegate void acceptDelegate();
	public delegate void rejectDelegate();
	
	public static int buttonWidth = 50;
	public static int buttonHeight = 20;	
	
	private bool show;
	
	private acceptDelegate onAccept;
	private rejectDelegate onReject;
	
	private string title;
	private string content;
	
	private Rect position;
	
	public bool Show { get { return this.show; } set { show = value; } }
	
	public UIDialog ()
	{
		show = true;
	}
	
	public UIDialog (Rect position, string title, string content, acceptDelegate onAccept, rejectDelegate onReject)
	{
		this.show = true;
		this.onAccept = onAccept;
		this.onReject = onReject;
		this.title = title;
		this.content = content;
		this.position = position;
	}
	
	//Show the instanciate dialog
	public bool showDialog(){
		return yesNo(position, title, content, onAccept, onReject);
	}

	
	public bool yesNo(Rect position, string title, string content, acceptDelegate onAccept, rejectDelegate onReject){
		return YesNo(position, title, content, ref this.show, onAccept, onReject);
	}

	
	//Default Yes/No Dialog
	public static bool YesNo(Rect position, string title, string content, ref bool show, ref bool accepted){
		return YesNo(position, title, content, ref show, ref accepted, null, null, "Yes", "No");
	}
	
	//Default Yes/No Dialog with delegates
	public static bool YesNo(Rect position, string title, string content, ref bool show, acceptDelegate onAccept, rejectDelegate onReject){
		bool accepted = false;
		return YesNo(position, title, content, ref show, ref accepted, onAccept, onReject, "Yes", "No");
	}
	
	
	public static bool YesNo(Rect position, string title, string content, ref bool show, ref bool accepted, acceptDelegate onAccept, rejectDelegate onReject, string yesText, string noText){	
		bool res = false;
		
		if (show){
			GUI.Box(position, title);
			
			Rect labelPos = new Rect (position.x + 15, position.y + 25, position.width - 30, position.height - buttonHeight - 25 );
			
			GUI.Label(labelPos, content);
			
			Rect buttonYesPos = new Rect (	position.center.x - buttonWidth - 5, 
											position.y + position.height - buttonHeight - 10, 
											buttonWidth, 
											buttonHeight);
			
			Rect buttonNoPos = new Rect(buttonYesPos);
			buttonNoPos.x = position.center.x + 5;
			
			if (GUI.Button(buttonYesPos, yesText)){
				accepted = true;
				res = true;
				show = false;
				if (onAccept != null)
					onAccept();
				
			}
			
			if (GUI.Button(buttonNoPos, noText)){
				accepted = false;
				res = false;
				show = false;
				if (onReject != null)
					onReject();
			}
		}
		
		return res;
	}
}
