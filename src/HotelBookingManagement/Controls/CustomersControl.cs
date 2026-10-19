using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HotelBookingManagement.Business.Interfaces;
using HotelBookingManagement.Business.Services;
using HotelBookingManagement.Models;

namespace HotelBookingManagement.Controls
{
	/// <summary>
	/// Giao diện Quản lý Khách hàng (TV02 - Screen 03 / Task CUS01 -> CUS04)
	/// Tuân thủ kiến trúc 3 lớp: Gọi ICustomerService, không truy vấn trực tiếp CSDL.
	/// </summary>
	public partial class CustomersControl : UserControl
	{
		private readonly ICustomerService _customerService;

		public CustomersControl()
		{
			InitializeComponent();
			_customerService = new CustomerService();
		}

		/// <summary>
		/// Constructor hỗ trợ Dependency Injection (phục vụ Unit Test hoặc Mocking)
		/// </summary>
		public CustomersControl(ICustomerService customerService)
		{
			InitializeComponent();
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}

		private void CustomersControl_Load(object? sender, EventArgs e)
		{
			// Cấu hình định dạng hiển thị cho các cột ngày tháng trên DataGridView
			colDateOfBirth.DefaultCellStyle.Format = "dd/MM/yyyy";
			colCreatedAt.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

			ResetForm();
			LoadCustomerData();
		}

		/// <summary>
		/// Tải danh sách khách hàng từ Service lên DataGridView
		/// </summary>
		private void LoadCustomerData(string? searchKeyword = null)
		{
			try
			{
				IEnumerable<Customer> customers;
				if (string.IsNullOrWhiteSpace(searchKeyword))
				{
					customers = _customerService.GetAllCustomers();
				}
				else
				{
					customers = _customerService.SearchCustomers(searchKeyword);
				}

				var list = customers.ToList();
				dgvCustomers.DataSource = list;
				lblTotalCount.Text = $"Tổng số: {list.Count} khách hàng";
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					$"Lỗi khi tải dữ liệu khách hàng: {ex.Message}",
					"Lỗi hệ thống",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Khi click chọn một dòng trên bảng -> Nạp dữ liệu lên Form để xem hoặc chỉnh sửa
		/// </summary>
		private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= dgvCustomers.Rows.Count) return;

			var row = dgvCustomers.Rows[e.RowIndex];
			if (row.DataBoundItem is Customer customer)
			{
				PopulateForm(customer);
			}
		}

		/// <summary>
		/// Nạp thông tin Customer vào các ô nhập
		/// </summary>
		private void PopulateForm(Customer customer)
		{
			txtCustomerId.Text = customer.CustomerId.ToString();
			txtFullName.Text = customer.FullName;
			txtPhone.Text = customer.Phone;
			txtIdentityNumber.Text = customer.IdentityNumber;
			txtEmail.Text = customer.Email ?? string.Empty;
			txtAddress.Text = customer.Address ?? string.Empty;

			if (customer.DateOfBirth.HasValue)
			{
				dtpDateOfBirth.Checked = true;
				dtpDateOfBirth.Value = customer.DateOfBirth.Value;
			}
			else
			{
				dtpDateOfBirth.Checked = false;
				dtpDateOfBirth.Value = DateTime.Today;
			}
		}

		/// <summary>
		/// Xử lý thêm mới khách hàng (Task CUS01 / CUS02)
		/// </summary>
		private void btnAdd_Click(object? sender, EventArgs e)
		{
			var customer = new Customer
			{
				FullName = txtFullName.Text.Trim(),
				Phone = txtPhone.Text.Trim(),
				IdentityNumber = txtIdentityNumber.Text.Trim(),
				Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
				Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
				DateOfBirth = dtpDateOfBirth.Checked ? dtpDateOfBirth.Value.Date : null
			};

			if (!_customerService.CreateCustomer(customer, out string errorMessage))
			{
				MessageBox.Show(
					errorMessage,
					"Kiểm tra thông tin",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			MessageBox.Show(
				$"Thêm mới khách hàng '{customer.FullName}' thành công!\nMã khách hàng: {customer.CustomerId}",
				"Thành công",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			LoadCustomerData();
			ResetForm();
		}

		/// <summary>
		/// Xử lý cập nhật thông tin khách hàng (Task CUS02)
		/// </summary>
		private void btnUpdate_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtCustomerId.Text, out int customerId) || customerId <= 0)
			{
				MessageBox.Show(
					"Vui lòng chọn một khách hàng từ danh sách bên phải để thực hiện cập nhật!",
					"Chưa chọn khách hàng",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			var customer = new Customer
			{
				CustomerId = customerId,
				FullName = txtFullName.Text.Trim(),
				Phone = txtPhone.Text.Trim(),
				IdentityNumber = txtIdentityNumber.Text.Trim(),
				Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
				Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
				DateOfBirth = dtpDateOfBirth.Checked ? dtpDateOfBirth.Value.Date : null
			};

			if (!_customerService.UpdateCustomer(customer, out string errorMessage))
			{
				MessageBox.Show(
					errorMessage,
					"Cập nhật thất bại",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			MessageBox.Show(
				$"Cập nhật thông tin khách hàng '{customer.FullName}' thành công!",
				"Thành công",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			LoadCustomerData();
			ResetForm();
		}

		/// <summary>
		/// Xử lý xóa khách hàng (Task CUS02)
		/// </summary>
		private void btnDelete_Click(object? sender, EventArgs e)
		{
			if (!int.TryParse(txtCustomerId.Text, out int customerId) || customerId <= 0)
			{
				MessageBox.Show(
					"Vui lòng chọn một khách hàng từ danh sách bên phải để xóa!",
					"Chưa chọn khách hàng",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return;
			}

			var confirm = MessageBox.Show(
				$"Bạn có chắc chắn muốn xóa khách hàng '{txtFullName.Text}' (Mã KH: {customerId}) không?\n\nLưu ý: Hành động này không thể hoàn tác!",
				"Xác nhận xóa khách hàng",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);

			if (confirm != DialogResult.Yes) return;

			if (!_customerService.DeleteCustomer(customerId, out string errorMessage))
			{
				MessageBox.Show(
					errorMessage,
					"Không thể xóa khách hàng",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}

			MessageBox.Show(
				"Đã xóa khách hàng thành công!",
				"Thành công",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);

			LoadCustomerData();
			ResetForm();
		}

		/// <summary>
		/// Làm mới form nhập liệu
		/// </summary>
		private void btnClear_Click(object? sender, EventArgs e)
		{
			ResetForm();
		}

		/// <summary>
		/// Tìm kiếm khách hàng theo từ khóa (Task CUS04)
		/// </summary>
		private void btnSearch_Click(object? sender, EventArgs e)
		{
			LoadCustomerData(txtSearch.Text);
		}

		/// <summary>
		/// Hỗ trợ gõ Enter trên ô tìm kiếm
		/// </summary>
		private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				btnSearch_Click(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Tải lại toàn bộ danh sách khách hàng
		/// </summary>
		private void btnRefresh_Click(object? sender, EventArgs e)
		{
			txtSearch.Clear();
			LoadCustomerData();
		}

		/// <summary>
		/// Đặt lại các trường nhập liệu về trạng thái ban đầu
		/// </summary>
		private void ResetForm()
		{
			txtCustomerId.Text = "(Tự động sinh)";
			txtFullName.Clear();
			txtPhone.Clear();
			txtIdentityNumber.Clear();
			txtEmail.Clear();
			txtAddress.Clear();
			dtpDateOfBirth.Value = DateTime.Today;
			dtpDateOfBirth.Checked = false;

			dgvCustomers.ClearSelection();
			txtFullName.Focus();
		}
	}
}
