using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CircularArray<T>
    {
        private T[] _buffer;
        private int _size;
        private int _beginIndex;
        private int _endIndex;

        private int EndIndex
        {
            get => _endIndex;
            set => _endIndex = value % _size;
        }

        public int Length => (EndIndex + _size - _beginIndex) % _size;
        public bool Empty => Length == 0;
        public bool Any => !Empty;

        public CircularArray(int size)
        {
            _buffer = new T[size];
            _size = size;
        }

        public void Write(T[] data)
        {
            Write(data, 0, data.Length);
        }

        public void Write(T[] data, int offset, int count)
        {
            for (var i = offset; i < offset + count; i++)
            {
                _buffer[EndIndex] = data[i];
                EndIndex++;
            }
        }

        public int Read(T[] arr)
        {
            return Read(arr, 0, arr.Length);
        }

        public int Read(T[] arr, int offset, int count)
        {
            var readCount = 0;
            for (var i = offset; Any && i < offset + count; i++)
            {
                arr[i] = _buffer[_beginIndex + readCount];
                readCount++;
            }
            _beginIndex = (_beginIndex + readCount) % _size;
            return readCount;
        }

        public Memory<T> GetWritableMemory()
        {
            if (_beginIndex <= EndIndex)
            {
                // 0    begin    end    size
                return new Memory<T>(_buffer, EndIndex, _size - EndIndex);
            }
            else
            {
                // 0    end        begin   size
                return new Memory<T>(_buffer, EndIndex, _beginIndex - EndIndex);
            }
        }

        public void ShiftEndIndex(int count)
        {
            EndIndex += count;
        }
    }
}
