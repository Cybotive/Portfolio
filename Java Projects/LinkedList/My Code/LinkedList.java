//Joshua Lini 3-7-19 - Written from scratch
public class LinkedList {
	
	private Node head = null;
	private Node curr = null;
	
	private int size = 0;
	
	LinkedList() {
		head = new Node();
	}
	
	public void add(Integer data) {
		
		if(head.data == null) { //changed to head.data since head was initialized
			head = new Node(data);
		}
		else {
			curr = head;
			while(curr.next != null){curr = curr.next;} //grab the end node of the list
			
			curr.next = new Node(data);
		}
		
		size++;
	}
	
	public void addSorted(Integer data) {

		if(head.data == null) { //changed to head.data since head was initialized
			head = new Node(data);
		}
		else {
			curr = head;
			Node prev = null;
			
			while(curr != null) {
				
				if(curr.data.compareTo(data) >= 0) { //if data is less than or equal to curr.data, and we're not skipping any values between the two
					if(prev == null) { //curr still points to head
						Node temp = new Node(data);
						temp.next = head;//keep the list away from garbage collector
						head = temp;//reassign front node
					}
					else { //curr doesn't point to head
						Node temp = new Node(data);
						temp.next = curr;//keep the end of the list away from garbage collector
						prev.next = temp;//complete the insertion
					}
					
					break;
				}
				
				if(curr.next == null) { //time to add this node to the end
					curr.next = new Node(data);
					break;
				}
				
				prev = curr;
				curr = curr.next;
			}
			
		}
		
		size++;
	}
	
	public boolean remove(Integer target) {
		
		if(head == null) {
			return false;//nothing removed since there are no nodes
		}
		
		curr = head;
		Node prev = null;
		
		while(curr != null) {
			
			if(curr.data.compareTo(target) == 0) {
				
				if(prev == null) { //curr still points to head
					head = curr.next;//shift the whole list
				}
				else { //curr doesn't point to head
					prev.next = curr.next;//skip over curr
				}
				
				size--;
				return true;//removing first occurrence (only) successfully
			}
			
			prev = curr;
			curr = curr.next;
		}
		
		return false;//nothing removed
	}
	
	private Integer findSmallest(){
		Integer smallest = head.data;
		
		curr = head;
		while(curr != null) {
			
			if(curr.data.compareTo(smallest) < 0) {
				smallest = curr.data;
			}
			
			curr = curr.next;
		}
		
		return smallest;
	}
	
	public Integer valueAtIndex(int index) {
		if(index > size-1 || index < 0) {
			return null;
		}
		
		curr = head;
		for(int i = 0; i < index; i++) {
			curr = curr.next;
		}
		
		return curr.data;
	}
	
	public LinkedList sorted() {
		LinkedList sortedLL = new LinkedList();
		for(int i = 0; i < size; i++) {
			sortedLL.addSorted(valueAtIndex(i));
		}
		
		return sortedLL;
	}
	
	public int getSize() {
		return size;
	}
	
	public void printList() {
		curr = head;
		
		if(size == 0) {
			return;
		}
		
		while(curr != null) {
			System.out.println(curr.data);
			curr = curr.next;
		}
	}
	
	public void printListReversed() {
		if(size == 0) {
			return;
		}
		
		for(int i = size-1; i >= 0; i--) { //go through for each node
			curr = head;
			
			for(int a = 0; a < i; a++) { //get to location to print
				curr = curr.next;
			}
			
			System.out.println(curr.data);//print our target
		}
	}
	
	public void printListNth(int n) {
		curr = head;
		int count = 0;
		
		if(size == 0) {
			return;
		}
		
		while(curr != null) {
			if(count % n == 0) {
				System.out.println(curr.data);
			}
			
			curr = curr.next;
			count++;
		}
	}
	
}
