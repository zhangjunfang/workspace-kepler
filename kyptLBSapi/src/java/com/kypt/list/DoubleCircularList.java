/*******************************************************************************
 * @(#)BackQueueObject.java 2008-9-3
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.list;

/**
 * the back's collection , which application platform login
 * @author pudong
 */
public class DoubleCircularList {

    private ListObject header;

    private ListObject tail;

    private ListObject last;

    private int size;

    public synchronized int getSize() {
        return size;
    }

    public synchronized ListObject get() {

        if (size == 0) return null;

        if (last == null) {
            last = header;
        } else {
            last = last.next;
        }
        return last;
    }

    public synchronized void add(ListObject obj) {

        if (obj == null) {
            throw new NullPointerException("object is null.");
        }

        ListObject temp = new ListObject();
        temp.setKey(obj.getKey());
        obj = temp;

        if (header == null) {
            header = obj;
            tail = obj;
        } else {
            tail.next = obj;
            obj.previous = tail;
            tail = obj;
        }
        header.previous = tail;
        tail.next = header;
        size++;
    }

    public synchronized void remove(ListObject obj) {

        if (obj == null) {
            throw new NullPointerException("object is null.");
        }

        if (size == 0 || contain(obj) == false) {
            throw new RuntimeException("list doesn't contain this object whose key is "
                    + obj.getKey() + " list size:" + this.getSize() + ",content:" + this.show());
        }

        if (last != null && last.getKey().equals(obj.getKey())) {
            last = last.next;
        }

        int i = 0;

        for (ListObject node = header; i < size; node = node.next) {
            if (node != null && node.getKey().equals(obj.getKey())) {
                node.previous.next = node.next;
                node.next.previous = node.previous;
                // 如果被删除的节点是头节点时，需要给头节点重新赋值
                if (node == header) {
                    header = node.next;
                }
                if (node == tail) {
                    tail = tail.previous;
                }
                node = null;
                size--;
                break; // List中允许存储多个同样的对象，在删除的时候，只删除其中的一个即可，因此需要break;
            }
            i++;
        }

        // 如果列表中所有的元素都被删除掉时，需要将header、tail、last赋值为null
        if (size == 0) {
            reset();
        }
    }

    public synchronized boolean contain(ListObject obj) {

        if (obj == null) {
            throw new NullPointerException("obj is null.");
        }

        int i = 0;

        for (ListObject node = header; i < size; node = node.next) {
            if (node != null && node.getKey().equals(obj.getKey())) {
                return true;
            }
            i++;
        }
        return false;
    }

    public synchronized String show() {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        for (ListObject node = header; i < size; node = node.next) {
            sb.append(node.getKey() + ":" + node.hashCode() + ",");
            i++;
        }
        return sb.toString();
    }

    private void reset() {
        header = null;
        tail = null;
        last = null;
    }

}
