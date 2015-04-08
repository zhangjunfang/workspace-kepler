package com.ctfo.service;

import com.ctfo.exception.CtfoAppException;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class PaginationResult<T>
  implements Serializable
{
  private static final long serialVersionUID = 1L;
  public static int DEFAULT_PAGE_SIZE = 10;
  private int pageSize;
  private int start;
  private Collection<T> data;
  private String resultJudge;
  private int totalCount;

  public PaginationResult()
  {
    this(0, 0, DEFAULT_PAGE_SIZE, new ArrayList<T>());
  }

  public PaginationResult(int start, int totalSize, int pageSize, Collection<T> data)
  {
    this.pageSize = DEFAULT_PAGE_SIZE;

    this.pageSize = pageSize;
    this.start = start;
    this.totalCount = totalSize;
    this.data = data;
  }

  public PaginationResult(String result)
  {
    this.pageSize = DEFAULT_PAGE_SIZE;

    this.resultJudge = result;
  }

  public String getResultJudge() {
    return this.resultJudge;
  }

  public void setResultJudge(String resultJudge) {
    this.resultJudge = resultJudge;
  }

  public Collection<T> getData() {
    return this.data;
  }

  public void setData(Collection<T> data) {
    this.data = data;
  }

  public void setPageSize(int pageSize) {
    this.pageSize = pageSize;
  }

  public void setStart(int start) {
    this.start = start;
  }

  public int getTotalCount()
  {
    return this.totalCount;
  }

  public void setTotalCount(int totalCount) {
    this.totalCount = totalCount;
  }

  public int getTotalPageCount()
  {
    if (this.totalCount % this.pageSize == 0) {
      return (this.totalCount / this.pageSize);
    }
    return (this.totalCount / this.pageSize + 1);
  }

  public int getPageSize()
  {
    return this.pageSize;
  }

  public Collection<T> getResult()
  {
    return this.data;
  }

  public int getCurrentPageNo()
  {
    return (this.start / this.pageSize + 1);
  }

  public boolean hasNextPage()
  {
    return (getCurrentPageNo() < getTotalPageCount() - 1);
  }

  public boolean hasPreviousPage()
  {
    return (getCurrentPageNo() > 1);
  }

  protected static int getStartOfPage(int pageNo)
  {
    return getStartOfPage(pageNo, DEFAULT_PAGE_SIZE);
  }

  public static int getStartOfPage(int pageNo, int pageSize)
  {
    return ((pageNo - 1) * pageSize);
  }

  public int getCurrentPage() {
    return getCurrentPageNo();
  }

  public int getTotalPage() {
    return getTotalPageCount();
  }

  public int getStart() {
    return this.start;
  }

  public static PaginationResult setResult(String result)
    throws CtfoAppException
  {
    PaginationResult bean = new PaginationResult();
    bean.setResultJudge(result);
    return bean;
  }

  public static PaginationResult setSimpleData(Object object)
    throws CtfoAppException
  {
    PaginationResult bean = new PaginationResult();
    bean.setTotalCount(1);
    List list = new ArrayList();
    list.add(object);
    bean.setData(list);
    return bean;
  }

  public static PaginationResult setPageListData(List list, int totalCount, int pagesize, int startNum)
    throws CtfoAppException
  {
    PaginationResult bean = new PaginationResult();
    bean.setTotalCount(totalCount);
    bean.setPageSize(pagesize);
    bean.setTotalCount(totalCount);
    bean.setStart(startNum);
    bean.setData(list);
    return bean;
  }

  public static PaginationResult setListData(List list)
    throws CtfoAppException
  {
    PaginationResult bean = new PaginationResult();
    bean.setData(list);
    return bean;
  }
}