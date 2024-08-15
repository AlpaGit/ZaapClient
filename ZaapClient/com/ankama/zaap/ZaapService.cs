using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace com.ankama.zaap
{
	// Token: 0x0200005D RID: 93
	public class ZaapService
	{
		// Token: 0x0200005E RID: 94
		public interface ISync
		{
			// Token: 0x060002BE RID: 702
			string connect(string gameName, string releaseName, int instanceId, string hash);

			// Token: 0x060002BF RID: 703
			string auth_getGameToken(string gameSession, int gameId);

			// Token: 0x060002C0 RID: 704
			bool updater_isUpdateAvailable(string gameSession);

			// Token: 0x060002C1 RID: 705
			string settings_get(string gameSession, string key);

			// Token: 0x060002C2 RID: 706
			void settings_set(string gameSession, string key, string value);

			// Token: 0x060002C3 RID: 707
			string askForGuestComplete(string gameSession);

			// Token: 0x060002C4 RID: 708
			string userInfo_get(string gameSession);

			// Token: 0x060002C5 RID: 709
			void release_restartOnExit(string gameSession);

			// Token: 0x060002C6 RID: 710
			void release_exitAndRepair(string gameSession, bool restartAfterRepair);

			// Token: 0x060002C7 RID: 711
			string zaapVersion_get(string gameSession);

			// Token: 0x060002C8 RID: 712
			bool zaapMustUpdate_get(string gameSession);

			// Token: 0x060002C9 RID: 713
			bool payArticle(string gameSession, string shopApiKey, int articleId, OverlayPosition pos);

			// Token: 0x060002CA RID: 714
			bool payArticleWithPid(string gameSession, string shopApiKey, int articleId, int pid, OverlayPosition pos);

			// Token: 0x060002CB RID: 715
			bool hasPremiumAccess(string gameSession);

			// Token: 0x060002CC RID: 716
			string auth_getGameTokenWithWindowId(string gameSession, int gameId, int windowId);

			// Token: 0x060002CD RID: 717
			string system_addNotification(string gameSession, NotificationOptions notificationOptions);

			// Token: 0x060002CE RID: 718
			NotificationResult addNotification(string gameSession, NotificationParams notificationParams);
		}

		// Token: 0x0200005F RID: 95
		public interface Iface : ZaapService.ISync
		{
		}

		// Token: 0x02000060 RID: 96
		public class Client : IDisposable, ZaapService.Iface, ZaapService.ISync
		{
			// Token: 0x060002CF RID: 719 RVA: 0x0000BC34 File Offset: 0x00009E34
			public Client(TProtocol prot)
				: this(prot, prot)
			{
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x0000BC40 File Offset: 0x00009E40
			public Client(TProtocol iprot, TProtocol oprot)
			{
				this.iprot_ = iprot;
				this.oprot_ = oprot;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000BC58 File Offset: 0x00009E58
			public TProtocol InputProtocol
			{
				get
				{
					return this.iprot_;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000BC60 File Offset: 0x00009E60
			public TProtocol OutputProtocol
			{
				get
				{
					return this.oprot_;
				}
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000BC68 File Offset: 0x00009E68
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000BC74 File Offset: 0x00009E74
			protected virtual void Dispose(bool disposing)
			{
				if (!this._IsDisposed && disposing)
				{
					if (this.iprot_ != null)
					{
						((IDisposable)this.iprot_).Dispose();
					}
					if (this.oprot_ != null)
					{
						((IDisposable)this.oprot_).Dispose();
					}
				}
				this._IsDisposed = true;
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x0000BCC8 File Offset: 0x00009EC8
			public string connect(string gameName, string releaseName, int instanceId, string hash)
			{
				this.send_connect(gameName, releaseName, instanceId, hash);
				return this.recv_connect();
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x0000BCDC File Offset: 0x00009EDC
			public void send_connect(string gameName, string releaseName, int instanceId, string hash)
			{
				this.oprot_.WriteMessageBegin(new TMessage("connect", TMessageType.Call, this.seqid_));
				new ZaapService.connect_args
				{
					GameName = gameName,
					ReleaseName = releaseName,
					InstanceId = instanceId,
					Hash = hash
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000BD50 File Offset: 0x00009F50
			public string recv_connect()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.connect_result connect_result = new ZaapService.connect_result();
				connect_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (connect_result.__isset.success)
				{
					return connect_result.Success;
				}
				if (connect_result.__isset.error)
				{
					throw connect_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "connect failed: unknown result");
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000BDE8 File Offset: 0x00009FE8
			public string auth_getGameToken(string gameSession, int gameId)
			{
				this.send_auth_getGameToken(gameSession, gameId);
				return this.recv_auth_getGameToken();
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000BDF8 File Offset: 0x00009FF8
			public void send_auth_getGameToken(string gameSession, int gameId)
			{
				this.oprot_.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Call, this.seqid_));
				new ZaapService.auth_getGameToken_args
				{
					GameSession = gameSession,
					GameId = gameId
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000BE5C File Offset: 0x0000A05C
			public string recv_auth_getGameToken()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.auth_getGameToken_result auth_getGameToken_result = new ZaapService.auth_getGameToken_result();
				auth_getGameToken_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (auth_getGameToken_result.__isset.success)
				{
					return auth_getGameToken_result.Success;
				}
				if (auth_getGameToken_result.__isset.error)
				{
					throw auth_getGameToken_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "auth_getGameToken failed: unknown result");
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
			public bool updater_isUpdateAvailable(string gameSession)
			{
				this.send_updater_isUpdateAvailable(gameSession);
				return this.recv_updater_isUpdateAvailable();
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000BF04 File Offset: 0x0000A104
			public void send_updater_isUpdateAvailable(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Call, this.seqid_));
				new ZaapService.updater_isUpdateAvailable_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000BF64 File Offset: 0x0000A164
			public bool recv_updater_isUpdateAvailable()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.updater_isUpdateAvailable_result updater_isUpdateAvailable_result = new ZaapService.updater_isUpdateAvailable_result();
				updater_isUpdateAvailable_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (updater_isUpdateAvailable_result.__isset.success)
				{
					return updater_isUpdateAvailable_result.Success;
				}
				if (updater_isUpdateAvailable_result.__isset.error)
				{
					throw updater_isUpdateAvailable_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "updater_isUpdateAvailable failed: unknown result");
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000BFFC File Offset: 0x0000A1FC
			public string settings_get(string gameSession, string key)
			{
				this.send_settings_get(gameSession, key);
				return this.recv_settings_get();
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000C00C File Offset: 0x0000A20C
			public void send_settings_get(string gameSession, string key)
			{
				this.oprot_.WriteMessageBegin(new TMessage("settings_get", TMessageType.Call, this.seqid_));
				new ZaapService.settings_get_args
				{
					GameSession = gameSession,
					Key = key
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x0000C070 File Offset: 0x0000A270
			public string recv_settings_get()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.settings_get_result settings_get_result = new ZaapService.settings_get_result();
				settings_get_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (settings_get_result.__isset.success)
				{
					return settings_get_result.Success;
				}
				if (settings_get_result.__isset.error)
				{
					throw settings_get_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "settings_get failed: unknown result");
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000C108 File Offset: 0x0000A308
			public void settings_set(string gameSession, string key, string value)
			{
				this.send_settings_set(gameSession, key, value);
				this.recv_settings_set();
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000C11C File Offset: 0x0000A31C
			public void send_settings_set(string gameSession, string key, string value)
			{
				this.oprot_.WriteMessageBegin(new TMessage("settings_set", TMessageType.Call, this.seqid_));
				new ZaapService.settings_set_args
				{
					GameSession = gameSession,
					Key = key,
					Value = value
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000C188 File Offset: 0x0000A388
			public void recv_settings_set()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.settings_set_result settings_set_result = new ZaapService.settings_set_result();
				settings_set_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (settings_set_result.__isset.error)
				{
					throw settings_set_result.Error;
				}
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000C1FC File Offset: 0x0000A3FC
			public string askForGuestComplete(string gameSession)
			{
				this.send_askForGuestComplete(gameSession);
				return this.recv_askForGuestComplete();
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000C20C File Offset: 0x0000A40C
			public void send_askForGuestComplete(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("askForGuestComplete", TMessageType.Call, this.seqid_));
				new ZaapService.askForGuestComplete_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000C26C File Offset: 0x0000A46C
			public string recv_askForGuestComplete()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.askForGuestComplete_result askForGuestComplete_result = new ZaapService.askForGuestComplete_result();
				askForGuestComplete_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (askForGuestComplete_result.__isset.success)
				{
					return askForGuestComplete_result.Success;
				}
				if (askForGuestComplete_result.__isset.error)
				{
					throw askForGuestComplete_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "askForGuestComplete failed: unknown result");
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000C304 File Offset: 0x0000A504
			public string userInfo_get(string gameSession)
			{
				this.send_userInfo_get(gameSession);
				return this.recv_userInfo_get();
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0000C314 File Offset: 0x0000A514
			public void send_userInfo_get(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("userInfo_get", TMessageType.Call, this.seqid_));
				new ZaapService.userInfo_get_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000C374 File Offset: 0x0000A574
			public string recv_userInfo_get()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.userInfo_get_result userInfo_get_result = new ZaapService.userInfo_get_result();
				userInfo_get_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (userInfo_get_result.__isset.success)
				{
					return userInfo_get_result.Success;
				}
				if (userInfo_get_result.__isset.error)
				{
					throw userInfo_get_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "userInfo_get failed: unknown result");
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000C40C File Offset: 0x0000A60C
			public void release_restartOnExit(string gameSession)
			{
				this.send_release_restartOnExit(gameSession);
				this.recv_release_restartOnExit();
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000C41C File Offset: 0x0000A61C
			public void send_release_restartOnExit(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("release_restartOnExit", TMessageType.Call, this.seqid_));
				new ZaapService.release_restartOnExit_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000C47C File Offset: 0x0000A67C
			public void recv_release_restartOnExit()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.release_restartOnExit_result release_restartOnExit_result = new ZaapService.release_restartOnExit_result();
				release_restartOnExit_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (release_restartOnExit_result.__isset.error)
				{
					throw release_restartOnExit_result.Error;
				}
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
			public void release_exitAndRepair(string gameSession, bool restartAfterRepair)
			{
				this.send_release_exitAndRepair(gameSession, restartAfterRepair);
				this.recv_release_exitAndRepair();
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000C500 File Offset: 0x0000A700
			public void send_release_exitAndRepair(string gameSession, bool restartAfterRepair)
			{
				this.oprot_.WriteMessageBegin(new TMessage("release_exitAndRepair", TMessageType.Call, this.seqid_));
				new ZaapService.release_exitAndRepair_args
				{
					GameSession = gameSession,
					RestartAfterRepair = restartAfterRepair
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000C564 File Offset: 0x0000A764
			public void recv_release_exitAndRepair()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.release_exitAndRepair_result release_exitAndRepair_result = new ZaapService.release_exitAndRepair_result();
				release_exitAndRepair_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (release_exitAndRepair_result.__isset.error)
				{
					throw release_exitAndRepair_result.Error;
				}
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
			public string zaapVersion_get(string gameSession)
			{
				this.send_zaapVersion_get(gameSession);
				return this.recv_zaapVersion_get();
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000C5E8 File Offset: 0x0000A7E8
			public void send_zaapVersion_get(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("zaapVersion_get", TMessageType.Call, this.seqid_));
				new ZaapService.zaapVersion_get_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000C648 File Offset: 0x0000A848
			public string recv_zaapVersion_get()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.zaapVersion_get_result zaapVersion_get_result = new ZaapService.zaapVersion_get_result();
				zaapVersion_get_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (zaapVersion_get_result.__isset.success)
				{
					return zaapVersion_get_result.Success;
				}
				if (zaapVersion_get_result.__isset.error)
				{
					throw zaapVersion_get_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "zaapVersion_get failed: unknown result");
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
			public bool zaapMustUpdate_get(string gameSession)
			{
				this.send_zaapMustUpdate_get(gameSession);
				return this.recv_zaapMustUpdate_get();
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
			public void send_zaapMustUpdate_get(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("zaapMustUpdate_get", TMessageType.Call, this.seqid_));
				new ZaapService.zaapMustUpdate_get_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000C750 File Offset: 0x0000A950
			public bool recv_zaapMustUpdate_get()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.zaapMustUpdate_get_result zaapMustUpdate_get_result = new ZaapService.zaapMustUpdate_get_result();
				zaapMustUpdate_get_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (zaapMustUpdate_get_result.__isset.success)
				{
					return zaapMustUpdate_get_result.Success;
				}
				if (zaapMustUpdate_get_result.__isset.error)
				{
					throw zaapMustUpdate_get_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "zaapMustUpdate_get failed: unknown result");
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
			public bool payArticle(string gameSession, string shopApiKey, int articleId, OverlayPosition pos)
			{
				this.send_payArticle(gameSession, shopApiKey, articleId, pos);
				return this.recv_payArticle();
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000C7FC File Offset: 0x0000A9FC
			public void send_payArticle(string gameSession, string shopApiKey, int articleId, OverlayPosition pos)
			{
				this.oprot_.WriteMessageBegin(new TMessage("payArticle", TMessageType.Call, this.seqid_));
				new ZaapService.payArticle_args
				{
					GameSession = gameSession,
					ShopApiKey = shopApiKey,
					ArticleId = articleId,
					Pos = pos
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000C870 File Offset: 0x0000AA70
			public bool recv_payArticle()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.payArticle_result payArticle_result = new ZaapService.payArticle_result();
				payArticle_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (payArticle_result.__isset.success)
				{
					return payArticle_result.Success;
				}
				if (payArticle_result.__isset.error)
				{
					throw payArticle_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "payArticle failed: unknown result");
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000C908 File Offset: 0x0000AB08
			public bool payArticleWithPid(string gameSession, string shopApiKey, int articleId, int pid, OverlayPosition pos)
			{
				this.send_payArticleWithPid(gameSession, shopApiKey, articleId, pid, pos);
				return this.recv_payArticleWithPid();
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000C920 File Offset: 0x0000AB20
			public void send_payArticleWithPid(string gameSession, string shopApiKey, int articleId, int pid, OverlayPosition pos)
			{
				this.oprot_.WriteMessageBegin(new TMessage("payArticleWithPid", TMessageType.Call, this.seqid_));
				new ZaapService.payArticleWithPid_args
				{
					GameSession = gameSession,
					ShopApiKey = shopApiKey,
					ArticleId = articleId,
					Pid = pid,
					Pos = pos
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000C99C File Offset: 0x0000AB9C
			public bool recv_payArticleWithPid()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.payArticleWithPid_result payArticleWithPid_result = new ZaapService.payArticleWithPid_result();
				payArticleWithPid_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (payArticleWithPid_result.__isset.success)
				{
					return payArticleWithPid_result.Success;
				}
				if (payArticleWithPid_result.__isset.error)
				{
					throw payArticleWithPid_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "payArticleWithPid failed: unknown result");
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000CA34 File Offset: 0x0000AC34
			public bool hasPremiumAccess(string gameSession)
			{
				this.send_hasPremiumAccess(gameSession);
				return this.recv_hasPremiumAccess();
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000CA44 File Offset: 0x0000AC44
			public void send_hasPremiumAccess(string gameSession)
			{
				this.oprot_.WriteMessageBegin(new TMessage("hasPremiumAccess", TMessageType.Call, this.seqid_));
				new ZaapService.hasPremiumAccess_args
				{
					GameSession = gameSession
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x060002FE RID: 766 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
			public bool recv_hasPremiumAccess()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.hasPremiumAccess_result hasPremiumAccess_result = new ZaapService.hasPremiumAccess_result();
				hasPremiumAccess_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (hasPremiumAccess_result.__isset.success)
				{
					return hasPremiumAccess_result.Success;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "hasPremiumAccess failed: unknown result");
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000CB24 File Offset: 0x0000AD24
			public string auth_getGameTokenWithWindowId(string gameSession, int gameId, int windowId)
			{
				this.send_auth_getGameTokenWithWindowId(gameSession, gameId, windowId);
				return this.recv_auth_getGameTokenWithWindowId();
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000CB38 File Offset: 0x0000AD38
			public void send_auth_getGameTokenWithWindowId(string gameSession, int gameId, int windowId)
			{
				this.oprot_.WriteMessageBegin(new TMessage("auth_getGameTokenWithWindowId", TMessageType.Call, this.seqid_));
				new ZaapService.auth_getGameTokenWithWindowId_args
				{
					GameSession = gameSession,
					GameId = gameId,
					WindowId = windowId
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000CBA4 File Offset: 0x0000ADA4
			public string recv_auth_getGameTokenWithWindowId()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.auth_getGameTokenWithWindowId_result auth_getGameTokenWithWindowId_result = new ZaapService.auth_getGameTokenWithWindowId_result();
				auth_getGameTokenWithWindowId_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (auth_getGameTokenWithWindowId_result.__isset.success)
				{
					return auth_getGameTokenWithWindowId_result.Success;
				}
				if (auth_getGameTokenWithWindowId_result.__isset.error)
				{
					throw auth_getGameTokenWithWindowId_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "auth_getGameTokenWithWindowId failed: unknown result");
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000CC3C File Offset: 0x0000AE3C
			public string system_addNotification(string gameSession, NotificationOptions notificationOptions)
			{
				this.send_system_addNotification(gameSession, notificationOptions);
				return this.recv_system_addNotification();
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000CC4C File Offset: 0x0000AE4C
			public void send_system_addNotification(string gameSession, NotificationOptions notificationOptions)
			{
				this.oprot_.WriteMessageBegin(new TMessage("system_addNotification", TMessageType.Call, this.seqid_));
				new ZaapService.system_addNotification_args
				{
					GameSession = gameSession,
					NotificationOptions = notificationOptions
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
			public string recv_system_addNotification()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.system_addNotification_result system_addNotification_result = new ZaapService.system_addNotification_result();
				system_addNotification_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (system_addNotification_result.__isset.success)
				{
					return system_addNotification_result.Success;
				}
				if (system_addNotification_result.__isset.error)
				{
					throw system_addNotification_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "system_addNotification failed: unknown result");
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public NotificationResult addNotification(string gameSession, NotificationParams notificationParams)
			{
				this.send_addNotification(gameSession, notificationParams);
				return this.recv_addNotification();
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000CD58 File Offset: 0x0000AF58
			public void send_addNotification(string gameSession, NotificationParams notificationParams)
			{
				this.oprot_.WriteMessageBegin(new TMessage("addNotification", TMessageType.Call, this.seqid_));
				new ZaapService.addNotification_args
				{
					GameSession = gameSession,
					NotificationParams = notificationParams
				}.Write(this.oprot_);
				this.oprot_.WriteMessageEnd();
				this.oprot_.Transport.Flush();
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000CDBC File Offset: 0x0000AFBC
			public NotificationResult recv_addNotification()
			{
				if (this.iprot_.ReadMessageBegin().Type == TMessageType.Exception)
				{
					TApplicationException ex = TApplicationException.Read(this.iprot_);
					this.iprot_.ReadMessageEnd();
					throw ex;
				}
				ZaapService.addNotification_result addNotification_result = new ZaapService.addNotification_result();
				addNotification_result.Read(this.iprot_);
				this.iprot_.ReadMessageEnd();
				if (addNotification_result.__isset.success)
				{
					return addNotification_result.Success;
				}
				if (addNotification_result.__isset.error)
				{
					throw addNotification_result.Error;
				}
				throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "addNotification failed: unknown result");
			}

			// Token: 0x040001AD RID: 429
			protected TProtocol iprot_;

			// Token: 0x040001AE RID: 430
			protected TProtocol oprot_;

			// Token: 0x040001AF RID: 431
			protected int seqid_;

			// Token: 0x040001B0 RID: 432
			private bool _IsDisposed;
		}

		// Token: 0x02000061 RID: 97
		public class Processor : TProcessor
		{
			// Token: 0x06000308 RID: 776 RVA: 0x0000CE54 File Offset: 0x0000B054
			public Processor(ZaapService.ISync iface)
			{
				this.iface_ = iface;
				this.processMap_["connect"] = new ZaapService.Processor.ProcessFunction(this.connect_Process);
				this.processMap_["auth_getGameToken"] = new ZaapService.Processor.ProcessFunction(this.auth_getGameToken_Process);
				this.processMap_["updater_isUpdateAvailable"] = new ZaapService.Processor.ProcessFunction(this.updater_isUpdateAvailable_Process);
				this.processMap_["settings_get"] = new ZaapService.Processor.ProcessFunction(this.settings_get_Process);
				this.processMap_["settings_set"] = new ZaapService.Processor.ProcessFunction(this.settings_set_Process);
				this.processMap_["askForGuestComplete"] = new ZaapService.Processor.ProcessFunction(this.askForGuestComplete_Process);
				this.processMap_["userInfo_get"] = new ZaapService.Processor.ProcessFunction(this.userInfo_get_Process);
				this.processMap_["release_restartOnExit"] = new ZaapService.Processor.ProcessFunction(this.release_restartOnExit_Process);
				this.processMap_["release_exitAndRepair"] = new ZaapService.Processor.ProcessFunction(this.release_exitAndRepair_Process);
				this.processMap_["zaapVersion_get"] = new ZaapService.Processor.ProcessFunction(this.zaapVersion_get_Process);
				this.processMap_["zaapMustUpdate_get"] = new ZaapService.Processor.ProcessFunction(this.zaapMustUpdate_get_Process);
				this.processMap_["payArticle"] = new ZaapService.Processor.ProcessFunction(this.payArticle_Process);
				this.processMap_["payArticleWithPid"] = new ZaapService.Processor.ProcessFunction(this.payArticleWithPid_Process);
				this.processMap_["hasPremiumAccess"] = new ZaapService.Processor.ProcessFunction(this.hasPremiumAccess_Process);
				this.processMap_["auth_getGameTokenWithWindowId"] = new ZaapService.Processor.ProcessFunction(this.auth_getGameTokenWithWindowId_Process);
				this.processMap_["system_addNotification"] = new ZaapService.Processor.ProcessFunction(this.system_addNotification_Process);
				this.processMap_["addNotification"] = new ZaapService.Processor.ProcessFunction(this.addNotification_Process);
			}

			// Token: 0x06000309 RID: 777 RVA: 0x0000D058 File Offset: 0x0000B258
			public bool Process(TProtocol iprot, TProtocol oprot)
			{
				try
				{
					TMessage tmessage = iprot.ReadMessageBegin();
					ZaapService.Processor.ProcessFunction processFunction;
					this.processMap_.TryGetValue(tmessage.Name, out processFunction);
					if (processFunction == null)
					{
						TProtocolUtil.Skip(iprot, TType.Struct);
						iprot.ReadMessageEnd();
						TApplicationException ex = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + tmessage.Name + "'");
						oprot.WriteMessageBegin(new TMessage(tmessage.Name, TMessageType.Exception, tmessage.SeqID));
						ex.Write(oprot);
						oprot.WriteMessageEnd();
						oprot.Transport.Flush();
						return true;
					}
					processFunction(tmessage.SeqID, iprot, oprot);
				}
				catch (IOException)
				{
					return false;
				}
				return true;
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000D11C File Offset: 0x0000B31C
			public void connect_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.connect_args connect_args = new ZaapService.connect_args();
				connect_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.connect_result connect_result = new ZaapService.connect_result();
				try
				{
					try
					{
						connect_result.Success = this.iface_.connect(connect_args.GameName, connect_args.ReleaseName, connect_args.InstanceId, connect_args.Hash);
					}
					catch (ZaapError zaapError)
					{
						connect_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("connect", TMessageType.Reply, seqid));
					connect_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("connect", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600030B RID: 779 RVA: 0x0000D224 File Offset: 0x0000B424
			public void auth_getGameToken_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.auth_getGameToken_args auth_getGameToken_args = new ZaapService.auth_getGameToken_args();
				auth_getGameToken_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.auth_getGameToken_result auth_getGameToken_result = new ZaapService.auth_getGameToken_result();
				try
				{
					try
					{
						auth_getGameToken_result.Success = this.iface_.auth_getGameToken(auth_getGameToken_args.GameSession, auth_getGameToken_args.GameId);
					}
					catch (ZaapError zaapError)
					{
						auth_getGameToken_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Reply, seqid));
					auth_getGameToken_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("auth_getGameToken", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600030C RID: 780 RVA: 0x0000D320 File Offset: 0x0000B520
			public void updater_isUpdateAvailable_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.updater_isUpdateAvailable_args updater_isUpdateAvailable_args = new ZaapService.updater_isUpdateAvailable_args();
				updater_isUpdateAvailable_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.updater_isUpdateAvailable_result updater_isUpdateAvailable_result = new ZaapService.updater_isUpdateAvailable_result();
				try
				{
					try
					{
						updater_isUpdateAvailable_result.Success = this.iface_.updater_isUpdateAvailable(updater_isUpdateAvailable_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						updater_isUpdateAvailable_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Reply, seqid));
					updater_isUpdateAvailable_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("updater_isUpdateAvailable", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000D418 File Offset: 0x0000B618
			public void settings_get_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.settings_get_args settings_get_args = new ZaapService.settings_get_args();
				settings_get_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.settings_get_result settings_get_result = new ZaapService.settings_get_result();
				try
				{
					try
					{
						settings_get_result.Success = this.iface_.settings_get(settings_get_args.GameSession, settings_get_args.Key);
					}
					catch (ZaapError zaapError)
					{
						settings_get_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("settings_get", TMessageType.Reply, seqid));
					settings_get_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("settings_get", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600030E RID: 782 RVA: 0x0000D514 File Offset: 0x0000B714
			public void settings_set_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.settings_set_args settings_set_args = new ZaapService.settings_set_args();
				settings_set_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.settings_set_result settings_set_result = new ZaapService.settings_set_result();
				try
				{
					try
					{
						this.iface_.settings_set(settings_set_args.GameSession, settings_set_args.Key, settings_set_args.Value);
					}
					catch (ZaapError zaapError)
					{
						settings_set_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("settings_set", TMessageType.Reply, seqid));
					settings_set_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("settings_set", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000D610 File Offset: 0x0000B810
			public void askForGuestComplete_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.askForGuestComplete_args askForGuestComplete_args = new ZaapService.askForGuestComplete_args();
				askForGuestComplete_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.askForGuestComplete_result askForGuestComplete_result = new ZaapService.askForGuestComplete_result();
				try
				{
					try
					{
						askForGuestComplete_result.Success = this.iface_.askForGuestComplete(askForGuestComplete_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						askForGuestComplete_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("askForGuestComplete", TMessageType.Reply, seqid));
					askForGuestComplete_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("askForGuestComplete", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000D708 File Offset: 0x0000B908
			public void userInfo_get_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.userInfo_get_args userInfo_get_args = new ZaapService.userInfo_get_args();
				userInfo_get_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.userInfo_get_result userInfo_get_result = new ZaapService.userInfo_get_result();
				try
				{
					try
					{
						userInfo_get_result.Success = this.iface_.userInfo_get(userInfo_get_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						userInfo_get_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("userInfo_get", TMessageType.Reply, seqid));
					userInfo_get_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("userInfo_get", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000D800 File Offset: 0x0000BA00
			public void release_restartOnExit_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.release_restartOnExit_args release_restartOnExit_args = new ZaapService.release_restartOnExit_args();
				release_restartOnExit_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.release_restartOnExit_result release_restartOnExit_result = new ZaapService.release_restartOnExit_result();
				try
				{
					try
					{
						this.iface_.release_restartOnExit(release_restartOnExit_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						release_restartOnExit_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("release_restartOnExit", TMessageType.Reply, seqid));
					release_restartOnExit_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("release_restartOnExit", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
			public void release_exitAndRepair_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.release_exitAndRepair_args release_exitAndRepair_args = new ZaapService.release_exitAndRepair_args();
				release_exitAndRepair_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.release_exitAndRepair_result release_exitAndRepair_result = new ZaapService.release_exitAndRepair_result();
				try
				{
					try
					{
						this.iface_.release_exitAndRepair(release_exitAndRepair_args.GameSession, release_exitAndRepair_args.RestartAfterRepair);
					}
					catch (ZaapError zaapError)
					{
						release_exitAndRepair_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("release_exitAndRepair", TMessageType.Reply, seqid));
					release_exitAndRepair_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("release_exitAndRepair", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000313 RID: 787 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
			public void zaapVersion_get_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.zaapVersion_get_args zaapVersion_get_args = new ZaapService.zaapVersion_get_args();
				zaapVersion_get_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.zaapVersion_get_result zaapVersion_get_result = new ZaapService.zaapVersion_get_result();
				try
				{
					try
					{
						zaapVersion_get_result.Success = this.iface_.zaapVersion_get(zaapVersion_get_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						zaapVersion_get_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("zaapVersion_get", TMessageType.Reply, seqid));
					zaapVersion_get_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("zaapVersion_get", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000314 RID: 788 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
			public void zaapMustUpdate_get_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.zaapMustUpdate_get_args zaapMustUpdate_get_args = new ZaapService.zaapMustUpdate_get_args();
				zaapMustUpdate_get_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.zaapMustUpdate_get_result zaapMustUpdate_get_result = new ZaapService.zaapMustUpdate_get_result();
				try
				{
					try
					{
						zaapMustUpdate_get_result.Success = this.iface_.zaapMustUpdate_get(zaapMustUpdate_get_args.GameSession);
					}
					catch (ZaapError zaapError)
					{
						zaapMustUpdate_get_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("zaapMustUpdate_get", TMessageType.Reply, seqid));
					zaapMustUpdate_get_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("zaapMustUpdate_get", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
			public void payArticle_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.payArticle_args payArticle_args = new ZaapService.payArticle_args();
				payArticle_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.payArticle_result payArticle_result = new ZaapService.payArticle_result();
				try
				{
					try
					{
						payArticle_result.Success = this.iface_.payArticle(payArticle_args.GameSession, payArticle_args.ShopApiKey, payArticle_args.ArticleId, payArticle_args.Pos);
					}
					catch (ZaapError zaapError)
					{
						payArticle_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("payArticle", TMessageType.Reply, seqid));
					payArticle_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("payArticle", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000316 RID: 790 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
			public void payArticleWithPid_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.payArticleWithPid_args payArticleWithPid_args = new ZaapService.payArticleWithPid_args();
				payArticleWithPid_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.payArticleWithPid_result payArticleWithPid_result = new ZaapService.payArticleWithPid_result();
				try
				{
					try
					{
						payArticleWithPid_result.Success = this.iface_.payArticleWithPid(payArticleWithPid_args.GameSession, payArticleWithPid_args.ShopApiKey, payArticleWithPid_args.ArticleId, payArticleWithPid_args.Pid, payArticleWithPid_args.Pos);
					}
					catch (ZaapError zaapError)
					{
						payArticleWithPid_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("payArticleWithPid", TMessageType.Reply, seqid));
					payArticleWithPid_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("payArticleWithPid", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000317 RID: 791 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
			public void hasPremiumAccess_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.hasPremiumAccess_args hasPremiumAccess_args = new ZaapService.hasPremiumAccess_args();
				hasPremiumAccess_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.hasPremiumAccess_result hasPremiumAccess_result = new ZaapService.hasPremiumAccess_result();
				try
				{
					hasPremiumAccess_result.Success = this.iface_.hasPremiumAccess(hasPremiumAccess_args.GameSession);
					oprot.WriteMessageBegin(new TMessage("hasPremiumAccess", TMessageType.Reply, seqid));
					hasPremiumAccess_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("hasPremiumAccess", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000318 RID: 792 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
			public void auth_getGameTokenWithWindowId_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.auth_getGameTokenWithWindowId_args auth_getGameTokenWithWindowId_args = new ZaapService.auth_getGameTokenWithWindowId_args();
				auth_getGameTokenWithWindowId_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.auth_getGameTokenWithWindowId_result auth_getGameTokenWithWindowId_result = new ZaapService.auth_getGameTokenWithWindowId_result();
				try
				{
					try
					{
						auth_getGameTokenWithWindowId_result.Success = this.iface_.auth_getGameTokenWithWindowId(auth_getGameTokenWithWindowId_args.GameSession, auth_getGameTokenWithWindowId_args.GameId, auth_getGameTokenWithWindowId_args.WindowId);
					}
					catch (ZaapError zaapError)
					{
						auth_getGameTokenWithWindowId_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("auth_getGameTokenWithWindowId", TMessageType.Reply, seqid));
					auth_getGameTokenWithWindowId_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("auth_getGameTokenWithWindowId", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x06000319 RID: 793 RVA: 0x0000DFCC File Offset: 0x0000C1CC
			public void system_addNotification_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.system_addNotification_args system_addNotification_args = new ZaapService.system_addNotification_args();
				system_addNotification_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.system_addNotification_result system_addNotification_result = new ZaapService.system_addNotification_result();
				try
				{
					try
					{
						system_addNotification_result.Success = this.iface_.system_addNotification(system_addNotification_args.GameSession, system_addNotification_args.NotificationOptions);
					}
					catch (ZaapError zaapError)
					{
						system_addNotification_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("system_addNotification", TMessageType.Reply, seqid));
					system_addNotification_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("system_addNotification", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x0600031A RID: 794 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
			public void addNotification_Process(int seqid, TProtocol iprot, TProtocol oprot)
			{
				ZaapService.addNotification_args addNotification_args = new ZaapService.addNotification_args();
				addNotification_args.Read(iprot);
				iprot.ReadMessageEnd();
				ZaapService.addNotification_result addNotification_result = new ZaapService.addNotification_result();
				try
				{
					try
					{
						addNotification_result.Success = this.iface_.addNotification(addNotification_args.GameSession, addNotification_args.NotificationParams);
					}
					catch (ZaapError zaapError)
					{
						addNotification_result.Error = zaapError;
					}
					oprot.WriteMessageBegin(new TMessage("addNotification", TMessageType.Reply, seqid));
					addNotification_result.Write(oprot);
				}
				catch (TTransportException)
				{
					throw;
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine("Error occurred in processor:");
					Console.Error.WriteLine(ex.ToString());
					TApplicationException ex2 = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
					oprot.WriteMessageBegin(new TMessage("addNotification", TMessageType.Exception, seqid));
					ex2.Write(oprot);
				}
				oprot.WriteMessageEnd();
				oprot.Transport.Flush();
			}

			// Token: 0x040001B1 RID: 433
			private ZaapService.ISync iface_;

			// Token: 0x040001B2 RID: 434
			protected Dictionary<string, ZaapService.Processor.ProcessFunction> processMap_ = new Dictionary<string, ZaapService.Processor.ProcessFunction>();

			// Token: 0x02000062 RID: 98
			// (Invoke) Token: 0x0600031C RID: 796
			protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
		}

		// Token: 0x02000063 RID: 99
		[Serializable]
		public class connect_args : TBase, TAbstractBase
		{
			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000E1CC File Offset: 0x0000C3CC
			// (set) Token: 0x06000321 RID: 801 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
			public string GameName
			{
				get
				{
					return this._gameName;
				}
				set
				{
					this.__isset.gameName = true;
					this._gameName = value;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000322 RID: 802 RVA: 0x0000E1EC File Offset: 0x0000C3EC
			// (set) Token: 0x06000323 RID: 803 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
			public string ReleaseName
			{
				get
				{
					return this._releaseName;
				}
				set
				{
					this.__isset.releaseName = true;
					this._releaseName = value;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000E20C File Offset: 0x0000C40C
			// (set) Token: 0x06000325 RID: 805 RVA: 0x0000E214 File Offset: 0x0000C414
			public int InstanceId
			{
				get
				{
					return this._instanceId;
				}
				set
				{
					this.__isset.instanceId = true;
					this._instanceId = value;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000E22C File Offset: 0x0000C42C
			// (set) Token: 0x06000327 RID: 807 RVA: 0x0000E234 File Offset: 0x0000C434
			public string Hash
			{
				get
				{
					return this._hash;
				}
				set
				{
					this.__isset.hash = true;
					this._hash = value;
				}
			}

			// Token: 0x06000328 RID: 808 RVA: 0x0000E24C File Offset: 0x0000C44C
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						switch (tfield.ID)
						{
						case 1:
							if (tfield.Type == TType.String)
							{
								this.GameName = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 2:
							if (tfield.Type == TType.String)
							{
								this.ReleaseName = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 3:
							if (tfield.Type == TType.I32)
							{
								this.InstanceId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 4:
							if (tfield.Type == TType.String)
							{
								this.Hash = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						default:
							TProtocolUtil.Skip(iprot, tfield.Type);
							break;
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("connect_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameName != null && this.__isset.gameName)
					{
						tfield.Name = "gameName";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameName);
						oprot.WriteFieldEnd();
					}
					if (this.ReleaseName != null && this.__isset.releaseName)
					{
						tfield.Name = "releaseName";
						tfield.Type = TType.String;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.ReleaseName);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.instanceId)
					{
						tfield.Name = "instanceId";
						tfield.Type = TType.I32;
						tfield.ID = 3;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.InstanceId);
						oprot.WriteFieldEnd();
					}
					if (this.Hash != null && this.__isset.hash)
					{
						tfield.Name = "hash";
						tfield.Type = TType.String;
						tfield.ID = 4;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.Hash);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600032A RID: 810 RVA: 0x0000E550 File Offset: 0x0000C750
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("connect_args(");
				bool flag = true;
				if (this.GameName != null && this.__isset.gameName)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameName: ");
					stringBuilder.Append(this.GameName);
				}
				if (this.ReleaseName != null && this.__isset.releaseName)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("ReleaseName: ");
					stringBuilder.Append(this.ReleaseName);
				}
				if (this.__isset.instanceId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("InstanceId: ");
					stringBuilder.Append(this.InstanceId);
				}
				if (this.Hash != null && this.__isset.hash)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Hash: ");
					stringBuilder.Append(this.Hash);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001B3 RID: 435
			private string _gameName;

			// Token: 0x040001B4 RID: 436
			private string _releaseName;

			// Token: 0x040001B5 RID: 437
			private int _instanceId;

			// Token: 0x040001B6 RID: 438
			private string _hash;

			// Token: 0x040001B7 RID: 439
			public ZaapService.connect_args.Isset __isset;

			// Token: 0x02000064 RID: 100
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001B8 RID: 440
				public bool gameName;

				// Token: 0x040001B9 RID: 441
				public bool releaseName;

				// Token: 0x040001BA RID: 442
				public bool instanceId;

				// Token: 0x040001BB RID: 443
				public bool hash;
			}
		}

		// Token: 0x02000065 RID: 101
		[Serializable]
		public class connect_result : TBase, TAbstractBase
		{
			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E69C File Offset: 0x0000C89C
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0000E6A4 File Offset: 0x0000C8A4
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E6BC File Offset: 0x0000C8BC
			// (set) Token: 0x0600032F RID: 815 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000E6DC File Offset: 0x0000C8DC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000331 RID: 817 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("connect_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000332 RID: 818 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("connect_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001BC RID: 444
			private string _success;

			// Token: 0x040001BD RID: 445
			private ZaapError _error;

			// Token: 0x040001BE RID: 446
			public ZaapService.connect_result.Isset __isset;

			// Token: 0x02000066 RID: 102
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001BF RID: 447
				public bool success;

				// Token: 0x040001C0 RID: 448
				public bool error;
			}
		}

		// Token: 0x02000067 RID: 103
		[Serializable]
		public class auth_getGameToken_args : TBase, TAbstractBase
		{
			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000334 RID: 820 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
			// (set) Token: 0x06000335 RID: 821 RVA: 0x0000E9BC File Offset: 0x0000CBBC
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0000E9DC File Offset: 0x0000CBDC
			public int GameId
			{
				get
				{
					return this._gameId;
				}
				set
				{
					this.__isset.gameId = true;
					this._gameId = value;
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							if (id != 2)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.I32)
							{
								this.GameId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000339 RID: 825 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("auth_getGameToken_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.gameId)
					{
						tfield.Name = "gameId";
						tfield.Type = TType.I32;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.GameId);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600033A RID: 826 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("auth_getGameToken_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.__isset.gameId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameId: ");
					stringBuilder.Append(this.GameId);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001C1 RID: 449
			private string _gameSession;

			// Token: 0x040001C2 RID: 450
			private int _gameId;

			// Token: 0x040001C3 RID: 451
			public ZaapService.auth_getGameToken_args.Isset __isset;

			// Token: 0x02000068 RID: 104
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001C4 RID: 452
				public bool gameSession;

				// Token: 0x040001C5 RID: 453
				public bool gameId;
			}
		}

		// Token: 0x02000069 RID: 105
		[Serializable]
		public class auth_getGameToken_result : TBase, TAbstractBase
		{
			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600033C RID: 828 RVA: 0x0000EC8C File Offset: 0x0000CE8C
			// (set) Token: 0x0600033D RID: 829 RVA: 0x0000EC94 File Offset: 0x0000CE94
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0000ECAC File Offset: 0x0000CEAC
			// (set) Token: 0x0600033F RID: 831 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000ECCC File Offset: 0x0000CECC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("auth_getGameToken_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000342 RID: 834 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("auth_getGameToken_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001C6 RID: 454
			private string _success;

			// Token: 0x040001C7 RID: 455
			private ZaapError _error;

			// Token: 0x040001C8 RID: 456
			public ZaapService.auth_getGameToken_result.Isset __isset;

			// Token: 0x0200006A RID: 106
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001C9 RID: 457
				public bool success;

				// Token: 0x040001CA RID: 458
				public bool error;
			}
		}

		// Token: 0x0200006B RID: 107
		[Serializable]
		public class updater_isUpdateAvailable_args : TBase, TAbstractBase
		{
			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
			// (set) Token: 0x06000345 RID: 837 RVA: 0x0000EFAC File Offset: 0x0000D1AC
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000F07C File Offset: 0x0000D27C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("updater_isUpdateAvailable_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000F124 File Offset: 0x0000D324
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("updater_isUpdateAvailable_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001CB RID: 459
			private string _gameSession;

			// Token: 0x040001CC RID: 460
			public ZaapService.updater_isUpdateAvailable_args.Isset __isset;

			// Token: 0x0200006C RID: 108
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001CD RID: 461
				public bool gameSession;
			}
		}

		// Token: 0x0200006D RID: 109
		[Serializable]
		public class updater_isUpdateAvailable_result : TBase, TAbstractBase
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
			// (set) Token: 0x0600034B RID: 843 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
			public bool Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
			// (set) Token: 0x0600034D RID: 845 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x0600034E RID: 846 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.Bool)
						{
							this.Success = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("updater_isUpdateAvailable_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						tfield.Name = "Success";
						tfield.Type = TType.Bool;
						tfield.ID = 0;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.Success);
						oprot.WriteFieldEnd();
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0000F3CC File Offset: 0x0000D5CC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("updater_isUpdateAvailable_result(");
				bool flag = true;
				if (this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001CE RID: 462
			private bool _success;

			// Token: 0x040001CF RID: 463
			private ZaapError _error;

			// Token: 0x040001D0 RID: 464
			public ZaapService.updater_isUpdateAvailable_result.Isset __isset;

			// Token: 0x0200006E RID: 110
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001D1 RID: 465
				public bool success;

				// Token: 0x040001D2 RID: 466
				public bool error;
			}
		}

		// Token: 0x0200006F RID: 111
		[Serializable]
		public class settings_get_args : TBase, TAbstractBase
		{
			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
			// (set) Token: 0x06000353 RID: 851 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000354 RID: 852 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
			// (set) Token: 0x06000355 RID: 853 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
			public string Key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.__isset.key = true;
					this._key = value;
				}
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							if (id != 2)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.String)
							{
								this.Key = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("settings_get_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.Key != null && this.__isset.key)
					{
						tfield.Name = "key";
						tfield.Type = TType.String;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.Key);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000358 RID: 856 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("settings_get_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.Key != null && this.__isset.key)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Key: ");
					stringBuilder.Append(this.Key);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001D3 RID: 467
			private string _gameSession;

			// Token: 0x040001D4 RID: 468
			private string _key;

			// Token: 0x040001D5 RID: 469
			public ZaapService.settings_get_args.Isset __isset;

			// Token: 0x02000070 RID: 112
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001D6 RID: 470
				public bool gameSession;

				// Token: 0x040001D7 RID: 471
				public bool key;
			}
		}

		// Token: 0x02000071 RID: 113
		[Serializable]
		public class settings_get_result : TBase, TAbstractBase
		{
			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000F78C File Offset: 0x0000D98C
			// (set) Token: 0x0600035B RID: 859 RVA: 0x0000F794 File Offset: 0x0000D994
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0000F7AC File Offset: 0x0000D9AC
			// (set) Token: 0x0600035D RID: 861 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000F7CC File Offset: 0x0000D9CC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("settings_get_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("settings_get_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001D8 RID: 472
			private string _success;

			// Token: 0x040001D9 RID: 473
			private ZaapError _error;

			// Token: 0x040001DA RID: 474
			public ZaapService.settings_get_result.Isset __isset;

			// Token: 0x02000072 RID: 114
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001DB RID: 475
				public bool success;

				// Token: 0x040001DC RID: 476
				public bool error;
			}
		}

		// Token: 0x02000073 RID: 115
		[Serializable]
		public class settings_set_args : TBase, TAbstractBase
		{
			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
			// (set) Token: 0x06000363 RID: 867 RVA: 0x0000FAAC File Offset: 0x0000DCAC
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000FAC4 File Offset: 0x0000DCC4
			// (set) Token: 0x06000365 RID: 869 RVA: 0x0000FACC File Offset: 0x0000DCCC
			public string Key
			{
				get
				{
					return this._key;
				}
				set
				{
					this.__isset.key = true;
					this._key = value;
				}
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000FAE4 File Offset: 0x0000DCE4
			// (set) Token: 0x06000367 RID: 871 RVA: 0x0000FAEC File Offset: 0x0000DCEC
			public string Value
			{
				get
				{
					return this._value;
				}
				set
				{
					this.__isset.value = true;
					this._value = value;
				}
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000FB04 File Offset: 0x0000DD04
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						switch (tfield.ID)
						{
						case 1:
							if (tfield.Type == TType.String)
							{
								this.GameSession = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 2:
							if (tfield.Type == TType.String)
							{
								this.Key = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 3:
							if (tfield.Type == TType.String)
							{
								this.Value = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						default:
							TProtocolUtil.Skip(iprot, tfield.Type);
							break;
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000FC2C File Offset: 0x0000DE2C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("settings_set_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.Key != null && this.__isset.key)
					{
						tfield.Name = "key";
						tfield.Type = TType.String;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.Key);
						oprot.WriteFieldEnd();
					}
					if (this.Value != null && this.__isset.value)
					{
						tfield.Name = "value";
						tfield.Type = TType.String;
						tfield.ID = 3;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.Value);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0000FD84 File Offset: 0x0000DF84
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("settings_set_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.Key != null && this.__isset.key)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Key: ");
					stringBuilder.Append(this.Key);
				}
				if (this.Value != null && this.__isset.value)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Value: ");
					stringBuilder.Append(this.Value);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001DD RID: 477
			private string _gameSession;

			// Token: 0x040001DE RID: 478
			private string _key;

			// Token: 0x040001DF RID: 479
			private string _value;

			// Token: 0x040001E0 RID: 480
			public ZaapService.settings_set_args.Isset __isset;

			// Token: 0x02000074 RID: 116
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001E1 RID: 481
				public bool gameSession;

				// Token: 0x040001E2 RID: 482
				public bool key;

				// Token: 0x040001E3 RID: 483
				public bool value;
			}
		}

		// Token: 0x02000075 RID: 117
		[Serializable]
		public class settings_set_result : TBase, TAbstractBase
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600036C RID: 876 RVA: 0x0000FE90 File Offset: 0x0000E090
			// (set) Token: 0x0600036D RID: 877 RVA: 0x0000FE98 File Offset: 0x0000E098
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x0600036E RID: 878 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.Struct)
						{
							this.Error = new ZaapError();
							this.Error.Read(iprot);
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600036F RID: 879 RVA: 0x0000FF74 File Offset: 0x0000E174
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("settings_set_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0001001C File Offset: 0x0000E21C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("settings_set_result(");
				bool flag = true;
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001E4 RID: 484
			private ZaapError _error;

			// Token: 0x040001E5 RID: 485
			public ZaapService.settings_set_result.Isset __isset;

			// Token: 0x02000076 RID: 118
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001E6 RID: 486
				public bool error;
			}
		}

		// Token: 0x02000077 RID: 119
		[Serializable]
		public class askForGuestComplete_args : TBase, TAbstractBase
		{
			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000372 RID: 882 RVA: 0x000100B4 File Offset: 0x0000E2B4
			// (set) Token: 0x06000373 RID: 883 RVA: 0x000100BC File Offset: 0x0000E2BC
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x06000374 RID: 884 RVA: 0x000100D4 File Offset: 0x0000E2D4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000375 RID: 885 RVA: 0x0001018C File Offset: 0x0000E38C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("askForGuestComplete_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000376 RID: 886 RVA: 0x00010234 File Offset: 0x0000E434
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("askForGuestComplete_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001E7 RID: 487
			private string _gameSession;

			// Token: 0x040001E8 RID: 488
			public ZaapService.askForGuestComplete_args.Isset __isset;

			// Token: 0x02000078 RID: 120
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001E9 RID: 489
				public bool gameSession;
			}
		}

		// Token: 0x02000079 RID: 121
		[Serializable]
		public class askForGuestComplete_result : TBase, TAbstractBase
		{
			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000378 RID: 888 RVA: 0x000102B0 File Offset: 0x0000E4B0
			// (set) Token: 0x06000379 RID: 889 RVA: 0x000102B8 File Offset: 0x0000E4B8
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600037A RID: 890 RVA: 0x000102D0 File Offset: 0x0000E4D0
			// (set) Token: 0x0600037B RID: 891 RVA: 0x000102D8 File Offset: 0x0000E4D8
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x0600037C RID: 892 RVA: 0x000102F0 File Offset: 0x0000E4F0
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600037D RID: 893 RVA: 0x000103E8 File Offset: 0x0000E5E8
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("askForGuestComplete_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600037E RID: 894 RVA: 0x000104E8 File Offset: 0x0000E6E8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("askForGuestComplete_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001EA RID: 490
			private string _success;

			// Token: 0x040001EB RID: 491
			private ZaapError _error;

			// Token: 0x040001EC RID: 492
			public ZaapService.askForGuestComplete_result.Isset __isset;

			// Token: 0x0200007A RID: 122
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001ED RID: 493
				public bool success;

				// Token: 0x040001EE RID: 494
				public bool error;
			}
		}

		// Token: 0x0200007B RID: 123
		[Serializable]
		public class userInfo_get_args : TBase, TAbstractBase
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000380 RID: 896 RVA: 0x000105C8 File Offset: 0x0000E7C8
			// (set) Token: 0x06000381 RID: 897 RVA: 0x000105D0 File Offset: 0x0000E7D0
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000105E8 File Offset: 0x0000E7E8
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000383 RID: 899 RVA: 0x000106A0 File Offset: 0x0000E8A0
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("userInfo_get_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000384 RID: 900 RVA: 0x00010748 File Offset: 0x0000E948
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("userInfo_get_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001EF RID: 495
			private string _gameSession;

			// Token: 0x040001F0 RID: 496
			public ZaapService.userInfo_get_args.Isset __isset;

			// Token: 0x0200007C RID: 124
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001F1 RID: 497
				public bool gameSession;
			}
		}

		// Token: 0x0200007D RID: 125
		[Serializable]
		public class userInfo_get_result : TBase, TAbstractBase
		{
			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000386 RID: 902 RVA: 0x000107C4 File Offset: 0x0000E9C4
			// (set) Token: 0x06000387 RID: 903 RVA: 0x000107CC File Offset: 0x0000E9CC
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000388 RID: 904 RVA: 0x000107E4 File Offset: 0x0000E9E4
			// (set) Token: 0x06000389 RID: 905 RVA: 0x000107EC File Offset: 0x0000E9EC
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x0600038A RID: 906 RVA: 0x00010804 File Offset: 0x0000EA04
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600038B RID: 907 RVA: 0x000108FC File Offset: 0x0000EAFC
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("userInfo_get_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600038C RID: 908 RVA: 0x000109FC File Offset: 0x0000EBFC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("userInfo_get_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001F2 RID: 498
			private string _success;

			// Token: 0x040001F3 RID: 499
			private ZaapError _error;

			// Token: 0x040001F4 RID: 500
			public ZaapService.userInfo_get_result.Isset __isset;

			// Token: 0x0200007E RID: 126
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001F5 RID: 501
				public bool success;

				// Token: 0x040001F6 RID: 502
				public bool error;
			}
		}

		// Token: 0x0200007F RID: 127
		[Serializable]
		public class release_restartOnExit_args : TBase, TAbstractBase
		{
			// Token: 0x17000072 RID: 114
			// (get) Token: 0x0600038E RID: 910 RVA: 0x00010ADC File Offset: 0x0000ECDC
			// (set) Token: 0x0600038F RID: 911 RVA: 0x00010AE4 File Offset: 0x0000ECE4
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x06000390 RID: 912 RVA: 0x00010AFC File Offset: 0x0000ECFC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000391 RID: 913 RVA: 0x00010BB4 File Offset: 0x0000EDB4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("release_restartOnExit_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000392 RID: 914 RVA: 0x00010C5C File Offset: 0x0000EE5C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("release_restartOnExit_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001F7 RID: 503
			private string _gameSession;

			// Token: 0x040001F8 RID: 504
			public ZaapService.release_restartOnExit_args.Isset __isset;

			// Token: 0x02000080 RID: 128
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001F9 RID: 505
				public bool gameSession;
			}
		}

		// Token: 0x02000081 RID: 129
		[Serializable]
		public class release_restartOnExit_result : TBase, TAbstractBase
		{
			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000394 RID: 916 RVA: 0x00010CD8 File Offset: 0x0000EED8
			// (set) Token: 0x06000395 RID: 917 RVA: 0x00010CE0 File Offset: 0x0000EEE0
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00010CF8 File Offset: 0x0000EEF8
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.Struct)
						{
							this.Error = new ZaapError();
							this.Error.Read(iprot);
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00010DBC File Offset: 0x0000EFBC
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("release_restartOnExit_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000398 RID: 920 RVA: 0x00010E64 File Offset: 0x0000F064
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("release_restartOnExit_result(");
				bool flag = true;
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001FA RID: 506
			private ZaapError _error;

			// Token: 0x040001FB RID: 507
			public ZaapService.release_restartOnExit_result.Isset __isset;

			// Token: 0x02000082 RID: 130
			[Serializable]
			public struct Isset
			{
				// Token: 0x040001FC RID: 508
				public bool error;
			}
		}

		// Token: 0x02000083 RID: 131
		[Serializable]
		public class release_exitAndRepair_args : TBase, TAbstractBase
		{
			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600039A RID: 922 RVA: 0x00010EFC File Offset: 0x0000F0FC
			// (set) Token: 0x0600039B RID: 923 RVA: 0x00010F04 File Offset: 0x0000F104
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600039C RID: 924 RVA: 0x00010F1C File Offset: 0x0000F11C
			// (set) Token: 0x0600039D RID: 925 RVA: 0x00010F24 File Offset: 0x0000F124
			public bool RestartAfterRepair
			{
				get
				{
					return this._restartAfterRepair;
				}
				set
				{
					this.__isset.restartAfterRepair = true;
					this._restartAfterRepair = value;
				}
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00010F3C File Offset: 0x0000F13C
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							if (id != 2)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Bool)
							{
								this.RestartAfterRepair = iprot.ReadBool();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600039F RID: 927 RVA: 0x0001102C File Offset: 0x0000F22C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("release_exitAndRepair_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.restartAfterRepair)
					{
						tfield.Name = "restartAfterRepair";
						tfield.Type = TType.Bool;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.RestartAfterRepair);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x00011118 File Offset: 0x0000F318
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("release_exitAndRepair_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.__isset.restartAfterRepair)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("RestartAfterRepair: ");
					stringBuilder.Append(this.RestartAfterRepair);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x040001FD RID: 509
			private string _gameSession;

			// Token: 0x040001FE RID: 510
			private bool _restartAfterRepair;

			// Token: 0x040001FF RID: 511
			public ZaapService.release_exitAndRepair_args.Isset __isset;

			// Token: 0x02000084 RID: 132
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000200 RID: 512
				public bool gameSession;

				// Token: 0x04000201 RID: 513
				public bool restartAfterRepair;
			}
		}

		// Token: 0x02000085 RID: 133
		[Serializable]
		public class release_exitAndRepair_result : TBase, TAbstractBase
		{
			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x000111D4 File Offset: 0x0000F3D4
			// (set) Token: 0x060003A3 RID: 931 RVA: 0x000111DC File Offset: 0x0000F3DC
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x000111F4 File Offset: 0x0000F3F4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.Struct)
						{
							this.Error = new ZaapError();
							this.Error.Read(iprot);
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x000112B8 File Offset: 0x0000F4B8
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("release_exitAndRepair_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x00011360 File Offset: 0x0000F560
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("release_exitAndRepair_result(");
				bool flag = true;
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000202 RID: 514
			private ZaapError _error;

			// Token: 0x04000203 RID: 515
			public ZaapService.release_exitAndRepair_result.Isset __isset;

			// Token: 0x02000086 RID: 134
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000204 RID: 516
				public bool error;
			}
		}

		// Token: 0x02000087 RID: 135
		[Serializable]
		public class zaapVersion_get_args : TBase, TAbstractBase
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x000113F8 File Offset: 0x0000F5F8
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x00011400 File Offset: 0x0000F600
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00011418 File Offset: 0x0000F618
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003AB RID: 939 RVA: 0x000114D0 File Offset: 0x0000F6D0
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("zaapVersion_get_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00011578 File Offset: 0x0000F778
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("zaapVersion_get_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000205 RID: 517
			private string _gameSession;

			// Token: 0x04000206 RID: 518
			public ZaapService.zaapVersion_get_args.Isset __isset;

			// Token: 0x02000088 RID: 136
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000207 RID: 519
				public bool gameSession;
			}
		}

		// Token: 0x02000089 RID: 137
		[Serializable]
		public class zaapVersion_get_result : TBase, TAbstractBase
		{
			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060003AE RID: 942 RVA: 0x000115F4 File Offset: 0x0000F7F4
			// (set) Token: 0x060003AF RID: 943 RVA: 0x000115FC File Offset: 0x0000F7FC
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x00011614 File Offset: 0x0000F814
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001161C File Offset: 0x0000F81C
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x00011634 File Offset: 0x0000F834
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0001172C File Offset: 0x0000F92C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("zaapVersion_get_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0001182C File Offset: 0x0000FA2C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("zaapVersion_get_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000208 RID: 520
			private string _success;

			// Token: 0x04000209 RID: 521
			private ZaapError _error;

			// Token: 0x0400020A RID: 522
			public ZaapService.zaapVersion_get_result.Isset __isset;

			// Token: 0x0200008A RID: 138
			[Serializable]
			public struct Isset
			{
				// Token: 0x0400020B RID: 523
				public bool success;

				// Token: 0x0400020C RID: 524
				public bool error;
			}
		}

		// Token: 0x0200008B RID: 139
		[Serializable]
		public class zaapMustUpdate_get_args : TBase, TAbstractBase
		{
			// Token: 0x1700007A RID: 122
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001190C File Offset: 0x0000FB0C
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x00011914 File Offset: 0x0000FB14
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x0001192C File Offset: 0x0000FB2C
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x000119E4 File Offset: 0x0000FBE4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("zaapMustUpdate_get_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003BA RID: 954 RVA: 0x00011A8C File Offset: 0x0000FC8C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("zaapMustUpdate_get_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x0400020D RID: 525
			private string _gameSession;

			// Token: 0x0400020E RID: 526
			public ZaapService.zaapMustUpdate_get_args.Isset __isset;

			// Token: 0x0200008C RID: 140
			[Serializable]
			public struct Isset
			{
				// Token: 0x0400020F RID: 527
				public bool gameSession;
			}
		}

		// Token: 0x0200008D RID: 141
		[Serializable]
		public class zaapMustUpdate_get_result : TBase, TAbstractBase
		{
			// Token: 0x1700007B RID: 123
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00011B08 File Offset: 0x0000FD08
			// (set) Token: 0x060003BD RID: 957 RVA: 0x00011B10 File Offset: 0x0000FD10
			public bool Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00011B28 File Offset: 0x0000FD28
			// (set) Token: 0x060003BF RID: 959 RVA: 0x00011B30 File Offset: 0x0000FD30
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x00011B48 File Offset: 0x0000FD48
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.Bool)
						{
							this.Success = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00011C40 File Offset: 0x0000FE40
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("zaapMustUpdate_get_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						tfield.Name = "Success";
						tfield.Type = TType.Bool;
						tfield.ID = 0;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.Success);
						oprot.WriteFieldEnd();
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00011D34 File Offset: 0x0000FF34
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("zaapMustUpdate_get_result(");
				bool flag = true;
				if (this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000210 RID: 528
			private bool _success;

			// Token: 0x04000211 RID: 529
			private ZaapError _error;

			// Token: 0x04000212 RID: 530
			public ZaapService.zaapMustUpdate_get_result.Isset __isset;

			// Token: 0x0200008E RID: 142
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000213 RID: 531
				public bool success;

				// Token: 0x04000214 RID: 532
				public bool error;
			}
		}

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class payArticle_args : TBase, TAbstractBase
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x00011E08 File Offset: 0x00010008
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x00011E10 File Offset: 0x00010010
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x00011E28 File Offset: 0x00010028
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x00011E30 File Offset: 0x00010030
			public string ShopApiKey
			{
				get
				{
					return this._shopApiKey;
				}
				set
				{
					this.__isset.shopApiKey = true;
					this._shopApiKey = value;
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x00011E48 File Offset: 0x00010048
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x00011E50 File Offset: 0x00010050
			public int ArticleId
			{
				get
				{
					return this._articleId;
				}
				set
				{
					this.__isset.articleId = true;
					this._articleId = value;
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x060003CA RID: 970 RVA: 0x00011E68 File Offset: 0x00010068
			// (set) Token: 0x060003CB RID: 971 RVA: 0x00011E70 File Offset: 0x00010070
			public OverlayPosition Pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.__isset.pos = true;
					this._pos = value;
				}
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00011E88 File Offset: 0x00010088
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						switch (id + 3)
						{
						case 0:
							if (tfield.Type == TType.Struct)
							{
								this.Pos = new OverlayPosition();
								this.Pos.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 1:
							if (tfield.Type == TType.I32)
							{
								this.ArticleId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 2:
							if (tfield.Type == TType.String)
							{
								this.ShopApiKey = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 3:
							goto IL_11C;
						case 4:
							if (tfield.Type == TType.String)
							{
								this.GameSession = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						default:
							goto IL_11C;
						}
						IL_12E:
						iprot.ReadFieldEnd();
						continue;
						IL_11C:
						TProtocolUtil.Skip(iprot, tfield.Type);
						goto IL_12E;
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00011FFC File Offset: 0x000101FC
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("payArticle_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.Pos != null && this.__isset.pos)
					{
						tfield.Name = "pos";
						tfield.Type = TType.Struct;
						tfield.ID = -3;
						oprot.WriteFieldBegin(tfield);
						this.Pos.Write(oprot);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.articleId)
					{
						tfield.Name = "articleId";
						tfield.Type = TType.I32;
						tfield.ID = -2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.ArticleId);
						oprot.WriteFieldEnd();
					}
					if (this.ShopApiKey != null && this.__isset.shopApiKey)
					{
						tfield.Name = "shopApiKey";
						tfield.Type = TType.String;
						tfield.ID = -1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.ShopApiKey);
						oprot.WriteFieldEnd();
					}
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003CE RID: 974 RVA: 0x00012198 File Offset: 0x00010398
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("payArticle_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.ShopApiKey != null && this.__isset.shopApiKey)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("ShopApiKey: ");
					stringBuilder.Append(this.ShopApiKey);
				}
				if (this.__isset.articleId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("ArticleId: ");
					stringBuilder.Append(this.ArticleId);
				}
				if (this.Pos != null && this.__isset.pos)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Pos: ");
					stringBuilder.Append((this.Pos != null) ? this.Pos.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000215 RID: 533
			private string _gameSession;

			// Token: 0x04000216 RID: 534
			private string _shopApiKey;

			// Token: 0x04000217 RID: 535
			private int _articleId;

			// Token: 0x04000218 RID: 536
			private OverlayPosition _pos;

			// Token: 0x04000219 RID: 537
			public ZaapService.payArticle_args.Isset __isset;

			// Token: 0x02000090 RID: 144
			[Serializable]
			public struct Isset
			{
				// Token: 0x0400021A RID: 538
				public bool gameSession;

				// Token: 0x0400021B RID: 539
				public bool shopApiKey;

				// Token: 0x0400021C RID: 540
				public bool articleId;

				// Token: 0x0400021D RID: 541
				public bool pos;
			}
		}

		// Token: 0x02000091 RID: 145
		[Serializable]
		public class payArticle_result : TBase, TAbstractBase
		{
			// Token: 0x17000081 RID: 129
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x000122FC File Offset: 0x000104FC
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x00012304 File Offset: 0x00010504
			public bool Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001231C File Offset: 0x0001051C
			// (set) Token: 0x060003D3 RID: 979 RVA: 0x00012324 File Offset: 0x00010524
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x0001233C File Offset: 0x0001053C
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.Bool)
						{
							this.Success = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x00012434 File Offset: 0x00010634
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("payArticle_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						tfield.Name = "Success";
						tfield.Type = TType.Bool;
						tfield.ID = 0;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.Success);
						oprot.WriteFieldEnd();
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x00012528 File Offset: 0x00010728
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("payArticle_result(");
				bool flag = true;
				if (this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x0400021E RID: 542
			private bool _success;

			// Token: 0x0400021F RID: 543
			private ZaapError _error;

			// Token: 0x04000220 RID: 544
			public ZaapService.payArticle_result.Isset __isset;

			// Token: 0x02000092 RID: 146
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000221 RID: 545
				public bool success;

				// Token: 0x04000222 RID: 546
				public bool error;
			}
		}

		// Token: 0x02000093 RID: 147
		[Serializable]
		public class payArticleWithPid_args : TBase, TAbstractBase
		{
			// Token: 0x17000083 RID: 131
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x000125FC File Offset: 0x000107FC
			// (set) Token: 0x060003D9 RID: 985 RVA: 0x00012604 File Offset: 0x00010804
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0001261C File Offset: 0x0001081C
			// (set) Token: 0x060003DB RID: 987 RVA: 0x00012624 File Offset: 0x00010824
			public string ShopApiKey
			{
				get
				{
					return this._shopApiKey;
				}
				set
				{
					this.__isset.shopApiKey = true;
					this._shopApiKey = value;
				}
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0001263C File Offset: 0x0001083C
			// (set) Token: 0x060003DD RID: 989 RVA: 0x00012644 File Offset: 0x00010844
			public int ArticleId
			{
				get
				{
					return this._articleId;
				}
				set
				{
					this.__isset.articleId = true;
					this._articleId = value;
				}
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0001265C File Offset: 0x0001085C
			// (set) Token: 0x060003DF RID: 991 RVA: 0x00012664 File Offset: 0x00010864
			public int Pid
			{
				get
				{
					return this._pid;
				}
				set
				{
					this.__isset.pid = true;
					this._pid = value;
				}
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001267C File Offset: 0x0001087C
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x00012684 File Offset: 0x00010884
			public OverlayPosition Pos
			{
				get
				{
					return this._pos;
				}
				set
				{
					this.__isset.pos = true;
					this._pos = value;
				}
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x0001269C File Offset: 0x0001089C
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						switch (id + 4)
						{
						case 0:
							if (tfield.Type == TType.Struct)
							{
								this.Pos = new OverlayPosition();
								this.Pos.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 1:
							if (tfield.Type == TType.I32)
							{
								this.Pid = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 2:
							if (tfield.Type == TType.I32)
							{
								this.ArticleId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 3:
							if (tfield.Type == TType.String)
							{
								this.ShopApiKey = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 4:
							goto IL_150;
						case 5:
							if (tfield.Type == TType.String)
							{
								this.GameSession = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						default:
							goto IL_150;
						}
						IL_162:
						iprot.ReadFieldEnd();
						continue;
						IL_150:
						TProtocolUtil.Skip(iprot, tfield.Type);
						goto IL_162;
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x00012844 File Offset: 0x00010A44
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("payArticleWithPid_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.Pos != null && this.__isset.pos)
					{
						tfield.Name = "pos";
						tfield.Type = TType.Struct;
						tfield.ID = -4;
						oprot.WriteFieldBegin(tfield);
						this.Pos.Write(oprot);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.pid)
					{
						tfield.Name = "pid";
						tfield.Type = TType.I32;
						tfield.ID = -3;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.Pid);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.articleId)
					{
						tfield.Name = "articleId";
						tfield.Type = TType.I32;
						tfield.ID = -2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.ArticleId);
						oprot.WriteFieldEnd();
					}
					if (this.ShopApiKey != null && this.__isset.shopApiKey)
					{
						tfield.Name = "shopApiKey";
						tfield.Type = TType.String;
						tfield.ID = -1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.ShopApiKey);
						oprot.WriteFieldEnd();
					}
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x00012A28 File Offset: 0x00010C28
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("payArticleWithPid_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.ShopApiKey != null && this.__isset.shopApiKey)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("ShopApiKey: ");
					stringBuilder.Append(this.ShopApiKey);
				}
				if (this.__isset.articleId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("ArticleId: ");
					stringBuilder.Append(this.ArticleId);
				}
				if (this.__isset.pid)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Pid: ");
					stringBuilder.Append(this.Pid);
				}
				if (this.Pos != null && this.__isset.pos)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Pos: ");
					stringBuilder.Append((this.Pos != null) ? this.Pos.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000223 RID: 547
			private string _gameSession;

			// Token: 0x04000224 RID: 548
			private string _shopApiKey;

			// Token: 0x04000225 RID: 549
			private int _articleId;

			// Token: 0x04000226 RID: 550
			private int _pid;

			// Token: 0x04000227 RID: 551
			private OverlayPosition _pos;

			// Token: 0x04000228 RID: 552
			public ZaapService.payArticleWithPid_args.Isset __isset;

			// Token: 0x02000094 RID: 148
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000229 RID: 553
				public bool gameSession;

				// Token: 0x0400022A RID: 554
				public bool shopApiKey;

				// Token: 0x0400022B RID: 555
				public bool articleId;

				// Token: 0x0400022C RID: 556
				public bool pid;

				// Token: 0x0400022D RID: 557
				public bool pos;
			}
		}

		// Token: 0x02000095 RID: 149
		[Serializable]
		public class payArticleWithPid_result : TBase, TAbstractBase
		{
			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x00012BC8 File Offset: 0x00010DC8
			// (set) Token: 0x060003E7 RID: 999 RVA: 0x00012BD0 File Offset: 0x00010DD0
			public bool Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00012BE8 File Offset: 0x00010DE8
			// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00012BF0 File Offset: 0x00010DF0
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00012C08 File Offset: 0x00010E08
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.Bool)
						{
							this.Success = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x00012D00 File Offset: 0x00010F00
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("payArticleWithPid_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						tfield.Name = "Success";
						tfield.Type = TType.Bool;
						tfield.ID = 0;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.Success);
						oprot.WriteFieldEnd();
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00012DF4 File Offset: 0x00010FF4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("payArticleWithPid_result(");
				bool flag = true;
				if (this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x0400022E RID: 558
			private bool _success;

			// Token: 0x0400022F RID: 559
			private ZaapError _error;

			// Token: 0x04000230 RID: 560
			public ZaapService.payArticleWithPid_result.Isset __isset;

			// Token: 0x02000096 RID: 150
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000231 RID: 561
				public bool success;

				// Token: 0x04000232 RID: 562
				public bool error;
			}
		}

		// Token: 0x02000097 RID: 151
		[Serializable]
		public class hasPremiumAccess_args : TBase, TAbstractBase
		{
			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012EC8 File Offset: 0x000110C8
			// (set) Token: 0x060003EF RID: 1007 RVA: 0x00012ED0 File Offset: 0x000110D0
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00012EE8 File Offset: 0x000110E8
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x00012FA0 File Offset: 0x000111A0
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("hasPremiumAccess_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x00013048 File Offset: 0x00011248
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("hasPremiumAccess_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000233 RID: 563
			private string _gameSession;

			// Token: 0x04000234 RID: 564
			public ZaapService.hasPremiumAccess_args.Isset __isset;

			// Token: 0x02000098 RID: 152
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000235 RID: 565
				public bool gameSession;
			}
		}

		// Token: 0x02000099 RID: 153
		[Serializable]
		public class hasPremiumAccess_result : TBase, TAbstractBase
		{
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000130C4 File Offset: 0x000112C4
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x000130CC File Offset: 0x000112CC
			public bool Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x060003F6 RID: 1014 RVA: 0x000130E4 File Offset: 0x000112E4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						else if (tfield.Type == TType.Bool)
						{
							this.Success = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003F7 RID: 1015 RVA: 0x00013198 File Offset: 0x00011398
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("hasPremiumAccess_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						tfield.Name = "Success";
						tfield.Type = TType.Bool;
						tfield.ID = 0;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteBool(this.Success);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x060003F8 RID: 1016 RVA: 0x00013234 File Offset: 0x00011434
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("hasPremiumAccess_result(");
				bool flag = true;
				if (this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000236 RID: 566
			private bool _success;

			// Token: 0x04000237 RID: 567
			public ZaapService.hasPremiumAccess_result.Isset __isset;

			// Token: 0x0200009A RID: 154
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000238 RID: 568
				public bool success;
			}
		}

		// Token: 0x0200009B RID: 155
		[Serializable]
		public class auth_getGameTokenWithWindowId_args : TBase, TAbstractBase
		{
			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060003FA RID: 1018 RVA: 0x000132A8 File Offset: 0x000114A8
			// (set) Token: 0x060003FB RID: 1019 RVA: 0x000132B0 File Offset: 0x000114B0
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060003FC RID: 1020 RVA: 0x000132C8 File Offset: 0x000114C8
			// (set) Token: 0x060003FD RID: 1021 RVA: 0x000132D0 File Offset: 0x000114D0
			public int GameId
			{
				get
				{
					return this._gameId;
				}
				set
				{
					this.__isset.gameId = true;
					this._gameId = value;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003FE RID: 1022 RVA: 0x000132E8 File Offset: 0x000114E8
			// (set) Token: 0x060003FF RID: 1023 RVA: 0x000132F0 File Offset: 0x000114F0
			public int WindowId
			{
				get
				{
					return this._windowId;
				}
				set
				{
					this.__isset.windowId = true;
					this._windowId = value;
				}
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x00013308 File Offset: 0x00011508
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						switch (tfield.ID)
						{
						case 1:
							if (tfield.Type == TType.String)
							{
								this.GameSession = iprot.ReadString();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 2:
							if (tfield.Type == TType.I32)
							{
								this.GameId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						case 3:
							if (tfield.Type == TType.I32)
							{
								this.WindowId = iprot.ReadI32();
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							break;
						default:
							TProtocolUtil.Skip(iprot, tfield.Type);
							break;
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0001342C File Offset: 0x0001162C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("auth_getGameTokenWithWindowId_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.gameId)
					{
						tfield.Name = "gameId";
						tfield.Type = TType.I32;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.GameId);
						oprot.WriteFieldEnd();
					}
					if (this.__isset.windowId)
					{
						tfield.Name = "windowId";
						tfield.Type = TType.I32;
						tfield.ID = 3;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteI32(this.WindowId);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0001356C File Offset: 0x0001176C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("auth_getGameTokenWithWindowId_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.__isset.gameId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameId: ");
					stringBuilder.Append(this.GameId);
				}
				if (this.__isset.windowId)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("WindowId: ");
					stringBuilder.Append(this.WindowId);
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000239 RID: 569
			private string _gameSession;

			// Token: 0x0400023A RID: 570
			private int _gameId;

			// Token: 0x0400023B RID: 571
			private int _windowId;

			// Token: 0x0400023C RID: 572
			public ZaapService.auth_getGameTokenWithWindowId_args.Isset __isset;

			// Token: 0x0200009C RID: 156
			[Serializable]
			public struct Isset
			{
				// Token: 0x0400023D RID: 573
				public bool gameSession;

				// Token: 0x0400023E RID: 574
				public bool gameId;

				// Token: 0x0400023F RID: 575
				public bool windowId;
			}
		}

		// Token: 0x0200009D RID: 157
		[Serializable]
		public class auth_getGameTokenWithWindowId_result : TBase, TAbstractBase
		{
			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000404 RID: 1028 RVA: 0x00013664 File Offset: 0x00011864
			// (set) Token: 0x06000405 RID: 1029 RVA: 0x0001366C File Offset: 0x0001186C
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x00013684 File Offset: 0x00011884
			// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001368C File Offset: 0x0001188C
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x000136A4 File Offset: 0x000118A4
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x0001379C File Offset: 0x0001199C
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("auth_getGameTokenWithWindowId_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0001389C File Offset: 0x00011A9C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("auth_getGameTokenWithWindowId_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000240 RID: 576
			private string _success;

			// Token: 0x04000241 RID: 577
			private ZaapError _error;

			// Token: 0x04000242 RID: 578
			public ZaapService.auth_getGameTokenWithWindowId_result.Isset __isset;

			// Token: 0x0200009E RID: 158
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000243 RID: 579
				public bool success;

				// Token: 0x04000244 RID: 580
				public bool error;
			}
		}

		// Token: 0x0200009F RID: 159
		[Serializable]
		public class system_addNotification_args : TBase, TAbstractBase
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001397C File Offset: 0x00011B7C
			// (set) Token: 0x0600040D RID: 1037 RVA: 0x00013984 File Offset: 0x00011B84
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001399C File Offset: 0x00011B9C
			// (set) Token: 0x0600040F RID: 1039 RVA: 0x000139A4 File Offset: 0x00011BA4
			public NotificationOptions NotificationOptions
			{
				get
				{
					return this._notificationOptions;
				}
				set
				{
					this.__isset.notificationOptions = true;
					this._notificationOptions = value;
				}
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x000139BC File Offset: 0x00011BBC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							if (id != 2)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.NotificationOptions = new NotificationOptions();
								this.NotificationOptions.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x00013AB8 File Offset: 0x00011CB8
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("system_addNotification_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.NotificationOptions != null && this.__isset.notificationOptions)
					{
						tfield.Name = "notificationOptions";
						tfield.Type = TType.Struct;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						this.NotificationOptions.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x00013BB0 File Offset: 0x00011DB0
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("system_addNotification_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.NotificationOptions != null && this.__isset.notificationOptions)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("NotificationOptions: ");
					stringBuilder.Append((this.NotificationOptions != null) ? this.NotificationOptions.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000245 RID: 581
			private string _gameSession;

			// Token: 0x04000246 RID: 582
			private NotificationOptions _notificationOptions;

			// Token: 0x04000247 RID: 583
			public ZaapService.system_addNotification_args.Isset __isset;

			// Token: 0x020000A0 RID: 160
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000248 RID: 584
				public bool gameSession;

				// Token: 0x04000249 RID: 585
				public bool notificationOptions;
			}
		}

		// Token: 0x020000A1 RID: 161
		[Serializable]
		public class system_addNotification_result : TBase, TAbstractBase
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x00013C90 File Offset: 0x00011E90
			// (set) Token: 0x06000415 RID: 1045 RVA: 0x00013C98 File Offset: 0x00011E98
			public string Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x00013CB0 File Offset: 0x00011EB0
			// (set) Token: 0x06000417 RID: 1047 RVA: 0x00013CB8 File Offset: 0x00011EB8
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x00013CD0 File Offset: 0x00011ED0
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.Success = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x00013DC8 File Offset: 0x00011FC8
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("system_addNotification_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.String;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							oprot.WriteString(this.Success);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x00013EC8 File Offset: 0x000120C8
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("system_addNotification_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append(this.Success);
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x0400024A RID: 586
			private string _success;

			// Token: 0x0400024B RID: 587
			private ZaapError _error;

			// Token: 0x0400024C RID: 588
			public ZaapService.system_addNotification_result.Isset __isset;

			// Token: 0x020000A2 RID: 162
			[Serializable]
			public struct Isset
			{
				// Token: 0x0400024D RID: 589
				public bool success;

				// Token: 0x0400024E RID: 590
				public bool error;
			}
		}

		// Token: 0x020000A3 RID: 163
		[Serializable]
		public class addNotification_args : TBase, TAbstractBase
		{
			// Token: 0x17000095 RID: 149
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013FA8 File Offset: 0x000121A8
			// (set) Token: 0x0600041D RID: 1053 RVA: 0x00013FB0 File Offset: 0x000121B0
			public string GameSession
			{
				get
				{
					return this._gameSession;
				}
				set
				{
					this.__isset.gameSession = true;
					this._gameSession = value;
				}
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x00013FC8 File Offset: 0x000121C8
			// (set) Token: 0x0600041F RID: 1055 RVA: 0x00013FD0 File Offset: 0x000121D0
			public NotificationParams NotificationParams
			{
				get
				{
					return this._notificationParams;
				}
				set
				{
					this.__isset.notificationParams = true;
					this._notificationParams = value;
				}
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x00013FE8 File Offset: 0x000121E8
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 1)
						{
							if (id != 2)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.NotificationParams = new NotificationParams();
								this.NotificationParams.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.String)
						{
							this.GameSession = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x000140E4 File Offset: 0x000122E4
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("addNotification_args");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.GameSession != null && this.__isset.gameSession)
					{
						tfield.Name = "gameSession";
						tfield.Type = TType.String;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						oprot.WriteString(this.GameSession);
						oprot.WriteFieldEnd();
					}
					if (this.NotificationParams != null && this.__isset.notificationParams)
					{
						tfield.Name = "notificationParams";
						tfield.Type = TType.Struct;
						tfield.ID = 2;
						oprot.WriteFieldBegin(tfield);
						this.NotificationParams.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x000141DC File Offset: 0x000123DC
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("addNotification_args(");
				bool flag = true;
				if (this.GameSession != null && this.__isset.gameSession)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("GameSession: ");
					stringBuilder.Append(this.GameSession);
				}
				if (this.NotificationParams != null && this.__isset.notificationParams)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("NotificationParams: ");
					stringBuilder.Append((this.NotificationParams != null) ? this.NotificationParams.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x0400024F RID: 591
			private string _gameSession;

			// Token: 0x04000250 RID: 592
			private NotificationParams _notificationParams;

			// Token: 0x04000251 RID: 593
			public ZaapService.addNotification_args.Isset __isset;

			// Token: 0x020000A4 RID: 164
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000252 RID: 594
				public bool gameSession;

				// Token: 0x04000253 RID: 595
				public bool notificationParams;
			}
		}

		// Token: 0x020000A5 RID: 165
		[Serializable]
		public class addNotification_result : TBase, TAbstractBase
		{
			// Token: 0x17000097 RID: 151
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x000142BC File Offset: 0x000124BC
			// (set) Token: 0x06000425 RID: 1061 RVA: 0x000142C4 File Offset: 0x000124C4
			public NotificationResult Success
			{
				get
				{
					return this._success;
				}
				set
				{
					this.__isset.success = true;
					this._success = value;
				}
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x000142DC File Offset: 0x000124DC
			// (set) Token: 0x06000427 RID: 1063 RVA: 0x000142E4 File Offset: 0x000124E4
			public ZaapError Error
			{
				get
				{
					return this._error;
				}
				set
				{
					this.__isset.error = true;
					this._error = value;
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x000142FC File Offset: 0x000124FC
			public void Read(TProtocol iprot)
			{
				iprot.IncrementRecursionDepth();
				try
				{
					iprot.ReadStructBegin();
					for (;;)
					{
						TField tfield = iprot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						short id = tfield.ID;
						if (id != 0)
						{
							if (id != 1)
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
							else if (tfield.Type == TType.Struct)
							{
								this.Error = new ZaapError();
								this.Error.Read(iprot);
							}
							else
							{
								TProtocolUtil.Skip(iprot, tfield.Type);
							}
						}
						else if (tfield.Type == TType.Struct)
						{
							this.Success = new NotificationResult();
							this.Success.Read(iprot);
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						iprot.ReadFieldEnd();
					}
					iprot.ReadStructEnd();
				}
				finally
				{
					iprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00014400 File Offset: 0x00012600
			public void Write(TProtocol oprot)
			{
				oprot.IncrementRecursionDepth();
				try
				{
					TStruct tstruct = new TStruct("addNotification_result");
					oprot.WriteStructBegin(tstruct);
					TField tfield = default(TField);
					if (this.__isset.success)
					{
						if (this.Success != null)
						{
							tfield.Name = "Success";
							tfield.Type = TType.Struct;
							tfield.ID = 0;
							oprot.WriteFieldBegin(tfield);
							this.Success.Write(oprot);
							oprot.WriteFieldEnd();
						}
					}
					else if (this.__isset.error && this.Error != null)
					{
						tfield.Name = "Error";
						tfield.Type = TType.Struct;
						tfield.ID = 1;
						oprot.WriteFieldBegin(tfield);
						this.Error.Write(oprot);
						oprot.WriteFieldEnd();
					}
					oprot.WriteFieldStop();
					oprot.WriteStructEnd();
				}
				finally
				{
					oprot.DecrementRecursionDepth();
				}
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00014500 File Offset: 0x00012700
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder("addNotification_result(");
				bool flag = true;
				if (this.Success != null && this.__isset.success)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					flag = false;
					stringBuilder.Append("Success: ");
					stringBuilder.Append((this.Success != null) ? this.Success.ToString() : "<null>");
				}
				if (this.Error != null && this.__isset.error)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append("Error: ");
					stringBuilder.Append((this.Error != null) ? this.Error.ToString() : "<null>");
				}
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000254 RID: 596
			private NotificationResult _success;

			// Token: 0x04000255 RID: 597
			private ZaapError _error;

			// Token: 0x04000256 RID: 598
			public ZaapService.addNotification_result.Isset __isset;

			// Token: 0x020000A6 RID: 166
			[Serializable]
			public struct Isset
			{
				// Token: 0x04000257 RID: 599
				public bool success;

				// Token: 0x04000258 RID: 600
				public bool error;
			}
		}
	}
}
